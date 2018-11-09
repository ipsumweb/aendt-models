using AENDiagnosticTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AENDiagnosticTracker.Models
{
    public class DiagnosticReport
    {
        public int ID { get; set; }

        public string PatientDemographicRaw { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Patient Name cannot be longer than 150 characters")]
        [Display(Name = "Patient Name", Prompt = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "Location cannot be more than 75 characters")]
        [Display(Name = "Location", Prompt = "Location")]
        public string Location { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "DRS cannot be more than 50 characters")]
        [Display(Name = "DRS", Prompt = "DRS")]
        public string DRS { get; set; }

        [Required]
        [Column(TypeName = "nchar(1)")]
        public Gender? Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DOB", Prompt = "Date of birth")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Image Captured Date", Prompt = "Image Captured Date/Time")]
        public DateTime? ImageCaptureDateTime { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Management Plan")]
        public RecommendationPlan ManagementPlan { get; set; }
        
        public int ManagementPlanID { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Referral Entity")]
        public RecommendationPlan ReferralEntity { get; set; }

        public int ReferralEntityID { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Referral Timeframe")]
        public RecommendationPlan ReferralTimeframe { get; set; }

        public int ReferralTimeframeID { get; set; }

        public string Comments { get; set; }

        [Required]
        [Display(Name = "Reviewer ID")]
        public int UserID { get; set; }

        [Display(Name = "Reviewer")]
        public User User { get; set; }

        [Display(Name = "Code")]
        [Column(TypeName = "nvarchar(50)")]
        public string PatientCode { get; set; }

        [Required]
        [Display(Name = "Report Create Date", Prompt = "Report Created Date")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string RightEyeOther { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string LeftEyeOther { get; set; }

//        private FaxAttempt faxAttempt;

        public ICollection<ReportCode> ReportCodes { get; set; }

        private class AssignedICD10CodeData
        {
            public int ICD10ID { get; set; }
            public string ICD10Code { get; set; }
            public string ICD10Summary { get; set; }
            public Eye ICD10Eye { get; set; }
            public bool Assigned { get; set; }
        }

        private List<AssignedICD10CodeData> AssignedICD10CodeDataList;

        public void PopulateAssignedICD10CodeData(AENDiagnosticContext _context, Models.DiagnosticReport diagnosticReport)
        {
            var allICD10Codes = _context.ICD10s;
            var reportICD10Codes = new HashSet<int>(diagnosticReport.ReportCodes.Select(c => c.ICD10ID));
            AssignedICD10CodeDataList = new List<AssignedICD10CodeData>();
            foreach (var icd10code in allICD10Codes)
            {
                AssignedICD10CodeDataList.Add(new AssignedICD10CodeData
                {
                    ICD10ID = icd10code.ID,
                    ICD10Code = icd10code.Code,
                    ICD10Summary = icd10code.Summary,
                    ICD10Eye = icd10code.Eye,
                    Assigned = reportICD10Codes.Contains(icd10code.ID)
                });
            }
        }


        public void UpdateDiagnosticReportICD10Codes(AENDiagnosticContext _context,
            string[] selectedICD10Codes, Models.DiagnosticReport DRToUpdate)
        {
            if (selectedICD10Codes == null)
            {
                DRToUpdate.ReportCodes = new List<ReportCode>();
                return;
            }

            var selectedICD10CodesHS = new HashSet<string>(selectedICD10Codes);
            var diagnosticReportICD10Codes = new HashSet<int>
                (DRToUpdate.ReportCodes.Select(c => c.ICD10.ID));
            foreach (var ICD10Code in _context.ICD10s)
            {
                if (selectedICD10CodesHS.Contains(ICD10Code.Code.ToString()))
                {
                    if (!diagnosticReportICD10Codes.Contains(ICD10Code.ID))
                    {
                        DRToUpdate.ReportCodes.Add(
                            new ReportCode
                            {
                                DiagnosticReportID = DRToUpdate.ID,
                                ICD10ID = ICD10Code.ID
                            });
                    }
                }
                else
                {
                    if (diagnosticReportICD10Codes.Contains(ICD10Code.ID))
                    {
                        ReportCode reportCodeToRemove
                            = DRToUpdate
                                .ReportCodes
                                .SingleOrDefault(i => i.ICD10ID == ICD10Code.ID);
                        _context.Remove(reportCodeToRemove);
                    }
                }
            }
        }
    }
}
