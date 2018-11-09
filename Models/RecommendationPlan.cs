using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AENDiagnosticTracker.Models
{

   public class RecommendationPlan
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Code type", Prompt ="Code type")]
        [StringLength(100, ErrorMessage = "Invalid code type.")]
        public string CodeType { get; set; }

        [Required]
        [Display(Name = "ICD Description", Prompt = "Description")]
        [StringLength(400, ErrorMessage = "Invalid Description.")]
        public string Description { get; set; }
    }

}
