using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int DiagnosisMasterId { get; set; }
        public int PatientVisitId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int PatientId { get; set; }
        public string DignosisDescription { get; set; }
    }
}
