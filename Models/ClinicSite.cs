using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AENDiagnosticTracker.Models
{

   public class ClinicSite
    {
        public int ID { get; set; }
        
        [Required]
        public int CameraID { get; set; } // 4 digit number
        
        
        [Required]
        [StringLength(50)]
        public string CameraCode { get; set; } // "drs-" plus CameraID

        [Required]
        [StringLength(50, ErrorMessage ="Invalid ICD Code, must be between 8 and 10 characters.")]
        public string LocationName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Email must be less than 50 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage ="Fax number must be exactly 10 digits; no special characters")]
        public string Fax { get; set; }

    }

}
