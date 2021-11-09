using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.Models
{
    public class AppointmentModel
    {

        public int appointmentId { get; set; }
        public int patientId { get; set; }
        public int physicianId { get; set; }
        public int? employeeId { get; set; }
        public string title { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public bool? isActive { get; set; }
        public int? createdBy { get; set; }
        public int? modifiedBy { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string physicianName { get; set; }
        public string patientName { get; set; }
    }
}
