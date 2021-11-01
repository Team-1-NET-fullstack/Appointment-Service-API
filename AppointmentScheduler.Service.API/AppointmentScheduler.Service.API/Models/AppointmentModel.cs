using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.Models
{
    public class AppointmentModel
    {
        public int? AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public string Reason { get; set; } // Title
        public int? TimeSlot { get; set; }
        public int? Duration { get; set; }
        public bool Status { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PhysicianName { get; set; }
        public string PatientName { get; set; }
        public int? PhysicianEmployeeId { get; set; }
    }
}
