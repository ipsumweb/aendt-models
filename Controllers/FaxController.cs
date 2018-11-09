using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AENDiagnosticTracker.Data;
using AENDiagnosticTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InterFAX.Api;
using InterFAX.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json.Linq;

namespace AENDiagnosticTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaxController : Controller
    {
        private readonly AENDiagnosticContext _context;

        private FaxClient interfax = new FaxClient("cmra226", "Hardw0rk!");
        // TODO - adjust code slightly so it sends as PCI compliant
        // apiRoot: FaxClient.ApiRoot.InterFAX_PCI, 

        public FaxController(AENDiagnosticContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<OutboundFaxSummary> GetFaxDetails(Int64 id)
        {
            return await interfax.Outbound.GetFaxRecord(id);
        }


        [HttpGet("balance")]
        public async Task<decimal> GetFaxAccountBalance()
        {
            return await interfax.Account.GetBalance();
        }


        [HttpGet("image/{id}")]
        public async Task<JsonResult> GetFaxImageStream(Int64 id)
        {
            Stream imageStream = await interfax.Outbound.GetFaxImageStream(id);
            
            // write stream to temp file
            string tempFile = Path.GetTempFileName() + ".tiff";
            using (FileStream stream = new FileStream(tempFile, FileMode.Create))
            {
                imageStream.CopyTo(stream);
            }

            // convert file to byte array and output as base64 image
            string path = tempFile;
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);

            return Json(string.Format("data:image/tiff;base64,{0}", imageBase64Data));
        }


        [HttpGet("attempts/{recordID}")]
        public async Task<List<FaxAttempt>> GetFaxAttempts(int recordID)
        {
            List<FaxAttempt> attempts = _context.FaxAttempts
                .Where(x => x.DiagnosticReportID == recordID)
                .OrderByDescending(x => x.CreateDate)
                .ToList();

//            foreach (FaxAttempt a in attempts)
//            {
//                a.FaxResult = await interfax.Outbound.GetFaxRecord(a.InterfaxID);  
//            }

            return attempts;
        }

        [HttpPost("{reportID}/{ifSimulateSuccess}")]
        public async Task SendFaxImage(int reportID, int ifSimulateSuccess = 1)
        {
            // parse the request body and get the base64 image content
            string body;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }

            // attempt to send the fax
            FaxAttempt faxAttempt = new FaxAttempt();
            long messageID = await faxAttempt.SendFax(body, ifSimulateSuccess == 1);

            // get details about the fax
            faxAttempt.InterfaxID = messageID;
            OutboundFaxSummary summary = new OutboundFaxSummary();
            summary = await interfax.Outbound.GetFaxRecord(messageID);

            faxAttempt.InterfaxID = summary.Id;
            faxAttempt.PagesSent = summary.PagesSent;
            faxAttempt.AttemptsMade = summary.AttemptsMade;
            faxAttempt.Status = summary.Status;
            faxAttempt.CreateDate = DateTime.Now;

            // save fax attempt record associated with this diagnostic report id
            faxAttempt.DiagnosticReportID = reportID;
            _context.Add(faxAttempt);
            await _context.SaveChangesAsync();
        }
    }
}