using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public int? EmployeeId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
