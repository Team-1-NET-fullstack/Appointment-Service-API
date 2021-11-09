using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class PatientMedicalDetail
    {
        public int PatientMedicalDetailsId { get; set; }
        public int PatientAllergyId { get; set; }
        public int DiagnosisId { get; set; }
        public int MedicationId { get; set; }
        public int ProcedureId { get; set; }
        public int PatientVisitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int PatientId { get; set; }
    }
}
