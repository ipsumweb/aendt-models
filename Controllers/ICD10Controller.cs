using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AENDiagnosticTracker.Data;
using AENDiagnosticTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AENDiagnosticTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ICD10Controller : Controller
    {
        private readonly AENDiagnosticContext _context;

        public ICD10Controller(AENDiagnosticContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<List<ICD10>> GetAll()
        {
            return _context.ICD10s
                .ToList();
        }


    }
}
