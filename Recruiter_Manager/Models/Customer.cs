using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public IEnumerable<Recruiter> Recruiters { get; set; }
        [NotMapped]
        public IEnumerable<JobPosting> JobPostings { get; set; }
        [NotMapped]
        public IEnumerable<Appointment> UpcomingAppointments { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
