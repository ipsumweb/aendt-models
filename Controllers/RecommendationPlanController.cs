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
    public class RecommendationPlanController : Controller
    {
        private readonly AENDiagnosticContext _context;

        public RecommendationPlanController(AENDiagnosticContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<RecommendationPlan>> GetAll()
        {
            return _context.RecommendationPlans
                .ToList();
        }
        
        [HttpGet("{recId}")]
        public ActionResult<RecommendationPlan> LookupForRecId(int recId)
        {
            // TODO - these s/b pulling as attributes of DiagnosticReport controller connected on the ID columns..
            return _context.RecommendationPlans
                .First(x => x.ID == recId);
        }

    }
}
