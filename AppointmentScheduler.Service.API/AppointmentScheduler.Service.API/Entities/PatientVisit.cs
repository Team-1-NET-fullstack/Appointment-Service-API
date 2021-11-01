using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class PatientVisit
    {
        public PatientVisit()
        {
            PatientVitals = new HashSet<PatientVital>();
        }

        public int PatientVisitId { get; set; }
        public string Reason { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; }
        public DateTime VisitDate { get; set; }
        public string DiagnosisDescription { get; set; }
        public string MedicationDescription { get; set; }
        public string ProcedureDescription { get; set; }
        public string AllergyDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual User Physician { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<PatientVital> PatientVitals { get; set; }
    }
}
