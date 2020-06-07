using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Models
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Interviewer First Name")]
        public string InterviewerFirstName { get; set; }
        [Display(Name = "Interviewer Last Name")]
        public string InterviewerLastName { get; set; }
        [Display(Name = "Interviewer Email")]
        public string InterviewerEmail { get; set; }
        [Display(Name = "Interviewer Phone Number")]
        public string InterviewerPhoneNumber { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Posted Compensation")]
        public string PostedComp { get; set; }
        [Display(Name = "Date Applied")]
        public DateTime DateApplied { get; set; }
        [Display(Name = "Application Status")]
        public string ApplicationStatus { get; set; }
        [Display(Name = "Conversation Notes")]
        public string ConversationNotes { get; set; }
        [NotMapped]
        public List<SelectListItem> ApplicationStatuses { get; set; }
        [NotMapped]
        public SelectList AppStatus { get; set; }
        [ForeignKey("Recruiter")]
        public int? RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public JobPosting()
        {
            ApplicationStatuses = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Application Submitted", Value = "1" },
                new SelectListItem() { Text = "Application Under Review", Value = "2" },
                new SelectListItem() { Text = "Interview Set", Value = "3" },
                new SelectListItem() { Text = "Offer Tendered", Value = "4" },
                new SelectListItem() { Text = "Counter Offer Submitted", Value = "5" },
                new SelectListItem() { Text = "Offer Accepted", Value = "6" },
                new SelectListItem() { Text = "Application Rejected", Value = "7" }
            };
        }
    }
}
