using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.RegularExpressions;
using InterFAX.Api;
using InterFAX.Api.Dtos;
using Newtonsoft.Json.Linq;

namespace AENDiagnosticTracker.Models
{

   public class FaxAttempt
    {
        private FaxClient interfax = new FaxClient("cmra226", "Hardw0rk!");
        // TODO - adjust code slightly so it sends as PCI compliant
        // apiRoot: FaxClient.ApiRoot.InterFAX_PCI, 
        
        public int ID { get; set; }
        
        [Required]
        public int DiagnosticReportID { get; set; }
        
        [Required]
        public long InterfaxID { get; set; } 
        
        [Required]
        public int PagesSent { get; set; } 
        
        [Required]
        public int AttemptsMade { get; set; } 
        
        [Required]
        public int Status { get; set; }
        
        [Required]
        public DateTime CreateDate { get; set; }

        //public OutboundFaxResult FaxResult { get; set; }
        
//        private DiagnosticReport report;

        
        public async Task<long> SendFax(string body, bool ifSimulateSuccess)
        {
            JObject result = JObject.Parse(body);

            var imageString = result["PageImage"].ToObject<string>();
            var base64Data = Regex.Match(imageString, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            byte[] image = Convert.FromBase64String(base64Data);

            // put the base64 data url into a temp file, for the fax to access
            string tempFile = Path.GetTempFileName();
            tempFile += ".jpg";

            using (FileStream stream = new FileStream(tempFile, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(image);
                    writer.Close();
                }
            }

            var faxNumber = ifSimulateSuccess ? "+999-9999-0" : "+999-9999-1";

            // send the fax
            // Error Code reference: https://www.interfax.net/en/help/error_codes
            var options = new SendOptions
            {
                FaxNumber = faxNumber,
                PageSize = PageSize.Letter,
                PageOrientation = PageOrientation.Portrait,
                RetriesToPerform = 3,
                PageResolution = PageResolution.Fine,
                ShouldScale = false
            };

            var fileDocument = interfax.Documents.BuildFaxDocument(tempFile);
            var messageId = await interfax.Outbound.SendFax(fileDocument, options);

            return messageId;
        }
        
        

    }

}
