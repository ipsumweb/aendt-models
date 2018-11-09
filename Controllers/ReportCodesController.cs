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
    public class ReportCodesController : Controller
    {
        private readonly AENDiagnosticContext _context;

        public ReportCodesController(AENDiagnosticContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ReportCode>> GetAll()
        {
            return _context.ReportCodes
                .ToList();
        }
        
        [HttpGet("{reportId}")]
        public ActionResult<List<ReportCode>> GetPerReport(int reportId)
        {
            return _context.ReportCodes
                .Where(x => x.DiagnosticReportID == reportId)
                .Include(x => x.ICD10)
                .ToList();
        }

    }
}
