using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiter_Manager.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Appointment Name")]
        public string AppointmentName { get; set; }
        [Display(Name="Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Display(Name="Appointment Details")]
        public string AppointmentDetails { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
    }
}
