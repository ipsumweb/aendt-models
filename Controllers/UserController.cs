using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AENDiagnosticTracker.Data;
using AENDiagnosticTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AENDiagnosticTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AENDiagnosticContext _context;

        public UserController(AENDiagnosticContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return _context.Users
                .ToList();
        }

//        [HttpGet("recent")]
//        public ActionResult<List<DiagnosticReport>> GetSome()
//        {
//            List<DiagnosticReport> list = _context.DiagnosticReports
//                .ToList();
//
//            list.OrderByDescending(x => x.CreateDate).Take(20);
//            return list;
//        }
//
        [HttpGet("{id}")]
        public ActionResult<User> GetOne(int id)
        {
            return _context.Users
                .Single(x => x.ID == id);
        }
//
//        [HttpGet("Search/{str}")]
//        public ActionResult<List<DiagnosticReport>> GetByTitleMatch(string str)
//        {
//            return _context.DiagnosticReports
//                .Where(x => x.PatientDemographicRaw.Contains(str))
//                .ToList();
//        }
//
//        [HttpPost]
//        public async Task<IActionResult> Insert()
//        {
//            string body;
//            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
//            {
//                body = await reader.ReadToEndAsync();
//            }
//
//            JObject report = JObject.Parse(body);
//
//            DiagnosticReport DRToInsert = new DiagnosticReport
//            {
//                CreateDate = DateTime.Now,
//                UserID = 6 // TODO - make dynamic; see note below
//            };
//
//            // logged in user - has to be a user id already in the system -- TODO - users dont login right now to use the app so this is hard coded
//            // DRToInsert.UserID = _context.Users.FirstOrDefault(m => m.LDAPID == System.Environment.UserName).ID;
//            /// 
//
//            // handle eyeball / report codes
//            DRToInsert.ReportCodes = new List<ReportCode>();
//            IList<JToken> codes = report["ReportCodes"].ToList();
//            foreach (JToken code in codes)
//            {
//                ReportCode c = code.ToObject<ReportCode>();
//                DRToInsert.ReportCodes.Add(c);
//            }
//
//            // a variation on handling OverPost issue
//            DRToInsert.PatientDemographicRaw = report["DiagnosticReport"]["PatientDemographicRaw"].ToObject<string>();
//            DRToInsert.PatientName = report["DiagnosticReport"]["PatientName"].ToObject<string>();
//            DRToInsert.Location = report["DiagnosticReport"]["Location"].ToObject<string>();
//            DRToInsert.DRS = report["DiagnosticReport"]["DRS"].ToObject<string>();
//            DRToInsert.Gender = report["DiagnosticReport"]["Gender"].ToObject<Gender>();
//            DRToInsert.DOB = report["DiagnosticReport"]["DOB"].ToObject<DateTime>();
//            DRToInsert.ImageCaptureDateTime = report["DiagnosticReport"]["ImageCaptureDateTime"].ToObject<DateTime>();
//            DRToInsert.Comments = report["DiagnosticReport"]["Comments"].ToObject<string>();
//            DRToInsert.PatientCode = report["DiagnosticReport"]["PatientCode"].ToObject<string>();
//            DRToInsert.LeftEyeOther = report["DiagnosticReport"]["LeftEyeOther"].ToObject<string>();
//            DRToInsert.RightEyeOther = report["DiagnosticReport"]["RightEyeOther"].ToObject<string>();
//
//            // management plan and referral plan info
//            int tempId = report["DiagnosticReport"]["ManagementPlanID"].ToObject<int>();
//            DRToInsert.ManagementPlan = _context.RecommendationPlans.FirstOrDefault(r => r.ID == tempId);
//
//            tempId = report["DiagnosticReport"]["ReferralEntityID"].ToObject<int>();
//            DRToInsert.ReferralEntity = _context.RecommendationPlans.FirstOrDefault(r => r.ID == tempId);
//
//            tempId = report["DiagnosticReport"]["ReferralTimeframeID"].ToObject<int>();
//            DRToInsert.ReferralTimeframe = _context.RecommendationPlans.FirstOrDefault(r => r.ID == tempId);
//
//            // send to the database
//            try
//            {
//                _context.Add(DRToInsert);
//                await _context.SaveChangesAsync();
//                return Ok();
//            }
//            catch (DbUpdateException)
//            {
//                return BadRequest();
//            }
//        }
    }
}