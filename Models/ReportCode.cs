using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AENDiagnosticTracker.Models
{
    public class ReportCode
    {
        public int DiagnosticReportID { get; set; }
        public int ICD10ID { get; set; }

        public DiagnosticReport DiagnosticReport { get; set; }
        public ICD10 ICD10 { get; set; }
    }
}
