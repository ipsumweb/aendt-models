using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AENDiagnosticTracker.Models
{
    public class User
    {
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        [RegularExpression(@"^[[a-zA-Z""'\s-]*$", ErrorMessage = "Please check the name entry and try again.")]
        [Display(Name = "First Name", Prompt = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$")]
        [Display(Name = "Last Name", Prompt = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters")]
        [Display(Name = "KY License Number", Prompt = "Ky License Number")]
        [Required]
        public string KYLicenseNumber { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters")]
        [Display(Name = "LDAP ID", Prompt = "LDAP ID")]
        [Required]
        public string LDAPID { get; set; }


        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        [NotMapped]
        public string FullUserName {
            get {
                return LastName + ", " + FirstName;
            }
        }

    }
}
