using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AENDiagnosticTracker.Models
{

   public class ICD10
    {
        public int ID { get; set; }

        [Required]
        [Display (Name="ICD Code", ShortName = "Code", Prompt ="Example: E12.3456")]
        [StringLength(10, MinimumLength = 8, ErrorMessage ="Invalid ICD Code, must be between 8 and 10 characters.")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "ICD Summary", Prompt ="ICD Summary")]
        [StringLength(100, ErrorMessage = "Invalid ICD Summary.")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "ICD Description", Prompt = "Description")]
        [StringLength(400, ErrorMessage = "Invalid Description.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Eye")]
        [Column(TypeName = "nchar(1)")]
        public Eye Eye { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }


    }

}
