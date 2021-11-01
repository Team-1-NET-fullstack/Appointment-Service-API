using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class PatientVital
    {
        public int PatientVitalId { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodPressure { get; set; }
        public double BodyTemperature { get; set; }
        public int RespirationRate { get; set; }
        public int PatientVisitId { get; set; }

        public virtual PatientVisit PatientVisit { get; set; }
    }
}
