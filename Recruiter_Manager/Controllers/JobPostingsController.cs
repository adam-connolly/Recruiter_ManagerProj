using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruiter_Manager.Contracts;
using Recruiter_Manager.Data;
using Recruiter_Manager.Models;

namespace Recruiter_Manager.Controllers
{
    public class JobPostingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IRepositoryWrapper _repo;

        public JobPostingsController(ApplicationDbContext context, IRepositoryWrapper repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: Recruiters
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.Customer.GetCustomer(userId);
            var applicationDbContext = _context.JobPostings.Where(r => r.CustomerId == customer.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recruiters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruiter = await _context.JobPostings
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // GET: Recruiters/Create
        public IActionResult Create()
        {
            JobPosting job = new JobPosting();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            job.AppStatus = new SelectList(job.ApplicationStatuses, "Text", "Text");     
            return View(job);
        }

        // POST: Recruiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,InterviewerFirstName,InterviewerLastName,InterviewerEmail,InterviewerPhoneNumber,CompanyName,PostedComp,DateApplied,ApplicationStatus,RecruiterId,CustomerId")] JobPosting job)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = _repo.Customer.GetCustomer(userId);
                job.CustomerId = customer.Id;
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Customers");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", job.CustomerId);
            return View(job);
        }

        // GET: Recruiters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.JobPostings.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", job.CustomerId);
            job.AppStatus = new SelectList(job.ApplicationStatuses, "Text", "Text");
            return View(job);
        }

        // POST: Recruiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,InterviewerFirstName,InterviewerLastName,InterviewerEmail,InterviewerPhoneNumber,CompanyName,PostedComp,DateApplied,ApplicationStatus,RecruiterId,CustomerId")] JobPosting job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            var jobToUpdate = _repo.JobPosting.FindByCondition(j => j.Id == id).SingleOrDefault();
            jobToUpdate.Id = id;
            jobToUpdate.CustomerId = customer.Id;
            jobToUpdate = UpdateJobPosting(job, jobToUpdate);
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.JobPostings.Update(jobToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostingExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Customers");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", job.CustomerId);
            return RedirectToAction("Index","Customers");
        }

        // GET: Recruiters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.JobPostings
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Recruiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.JobPostings.FindAsync(id);
            _context.JobPostings.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Customers");
        }

        private bool JobPostingExists(int id)
        {
            return _context.JobPostings.Any(e => e.Id == id);
        }
        private JobPosting UpdateJobPosting(JobPosting job, JobPosting jobToUpdate)
        {
            jobToUpdate.JobTitle = job.JobTitle;
            jobToUpdate.InterviewerFirstName = job.InterviewerFirstName;
            jobToUpdate.InterviewerLastName = job.InterviewerLastName;
            jobToUpdate.InterviewerEmail = job.InterviewerEmail;
            jobToUpdate.InterviewerPhoneNumber = job.InterviewerPhoneNumber;
            jobToUpdate.CompanyName = job.CompanyName;
            jobToUpdate.PostedComp = job.PostedComp;
            jobToUpdate.DateApplied = job.DateApplied;
            jobToUpdate.ApplicationStatus = job.ApplicationStatus;
            job.ConversationNotes = job.ConversationNotes;
            return jobToUpdate;
        }
    }
}
