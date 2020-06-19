using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruiter_Manager.Data;

namespace Recruiter_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AverageSalariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AverageSalariesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //// GET: api/AverageSalaries/5
        //[HttpGet("{jobTitle}", Name = "Get")]
        //public IActionResult GetByJobTitle(string jobTitle)
        //{
        //    var avgSalaries = _context.AverageSalaries.Where(s => s.JobTitle.Contains(jobTitle));
        //    return Ok(avgSalaries);
        //}
    }
}