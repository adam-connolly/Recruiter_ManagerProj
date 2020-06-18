using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruiter_Manager.Contracts;
using Recruiter_Manager.Data;
using Recruiter_Manager.Models;

namespace Recruiter_Manager.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IRepositoryWrapper _repo;
        public AppointmentsController(ApplicationDbContext context, IRepositoryWrapper repo)
        {
            _context = context;
            _repo = repo;
        }
        // GET: Appointments
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.Customer.GetCustomer(userId);
            var appointments = _context.Appointments.Where(a => a.CustomerId == customer.Id).ToList();
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int id)
        {
            var appointment = _context.Appointments.Where(a => a.Id == id).SingleOrDefault();
            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            Appointment appointment = new Appointment();
            return View(appointment);
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,AppointmentName,AppointmentDate,AppointmentDetails")] Appointment appointment)
        {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = _repo.Customer.GetCustomer(userId);
                appointment.CustomerId = customer.Id;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Customers");
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if(appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppointmentName,AppointmentDate,AppointmentDetails")] Appointment appointment)
        {
            if(id != appointment.Id)
            {
                return NotFound();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            var appointmentToUpdate = _context.Appointments.Where(a => a.Id == id).SingleOrDefault();
            appointmentToUpdate.Id = id;
            appointmentToUpdate.CustomerId = customer.Id;
            appointmentToUpdate = UpdateAppointment(appointment, appointmentToUpdate);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Appointments.Update(appointmentToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    
                }
                return RedirectToAction("Details", "Appointments");
            }
            return RedirectToAction("Index","Appointments");
        }

        private Appointment UpdateAppointment(Appointment appointment, Appointment appointmentToUpdate)
        {
            appointmentToUpdate.AppointmentName = appointment.AppointmentName;
            appointmentToUpdate.AppointmentDate = appointment.AppointmentDate;
            appointmentToUpdate.AppointmentDetails = appointment.AppointmentDetails;
            return appointmentToUpdate;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            var appointment = _context.Appointments.Where(a => a.Id == id);
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}