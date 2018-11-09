using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AENDiagnosticTracker.Models
{

    public class EnumModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }

    public enum Eye
    {
        [Display(Name = "LEFT")]
        L,

        [Display(Name = "RIGHT")]
        R,

        [Display(Name = "BOTH")]
        B
    }

    

    public enum Gender
    {
        [Display(Name = "Male")]
        M,

        [Display(Name = "Female")]
        F,

        [Display(Name = "Other")]
        O
    }
}
