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
        public string AppointmentName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentDetails { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
    }
}
