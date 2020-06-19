using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruiter_Manager.Contracts;
using Recruiter_Manager.Data;
using Recruiter_Manager.Models;
using Recruiter_Manager.Services;

namespace Recruiter_Manager.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IRepositoryWrapper _repo;
        private SalaryService _salaryService;

        public CustomersController(ApplicationDbContext context, IRepositoryWrapper repo, SalaryService salaryService)
        {
            _context = context;
            _repo = repo;
            _salaryService = salaryService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.Customer.GetCustomer(userId);
            if(customer == null)
            {
                return RedirectToAction("Create");
            }
            customer.Recruiters = _repo.Recruiter.FindByCondition(r => r.CustomerId == customer.Id).ToList();
            customer.JobPostings = _repo.JobPosting.FindByCondition(j => j.CustomerId == customer.Id).ToList();
            customer.UpcomingAppointments = _context.Appointments.Where(a => a.CustomerId == customer.Id).ToList();
            customer.UpcomingAppointments = GetNextWeekAppointments(customer);
            return View(customer);
        }
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            Customer customer = new Customer();
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,IdentityUserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
                customer.IdentityUserId = userId;
                customer.Email = user.Email;
                customer.PhoneNumber = StandardizePhoneNumber(customer.PhoneNumber);
                _repo.Customer.CreateCustomer(customer);
                await _context.SaveChangesAsync();
                
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.Customer.GetCustomer(userId);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,IdentityUserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    StandardizePhoneNumber(customer.PhoneNumber);
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return RedirectToAction("Edit", "Customers");
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        private string StandardizePhoneNumber(string phoneNumber)
        {
            var contactNumber = "";
            phoneNumber.ToCharArray();
            if (phoneNumber[0] != '+')
            {
                contactNumber += "+";
            }
            if(phoneNumber[0] != '1')
            {
                contactNumber += "1";
            }
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (phoneNumber[i] == '-' || phoneNumber[i] == '(' || phoneNumber[i] == ')' || phoneNumber[i] == ' ')
                {

                }
                else
                {
                    contactNumber += phoneNumber[i];
                }
            }

            return contactNumber;
        }
        private IEnumerable<Appointment> GetNextWeekAppointments(Customer customer)
        {
            var appts = new List<Appointment>();
            foreach(var appt in customer.UpcomingAppointments)
            {
                if(appt.AppointmentDate.Day >= DateTime.Today.Day && appt.AppointmentDate.Day <= (DateTime.Today.Day + 7))
                {
                    appts.Add(appt);
                }
            }
            customer.UpcomingAppointments = appts;
            return customer.UpcomingAppointments;
        }
    }
}
