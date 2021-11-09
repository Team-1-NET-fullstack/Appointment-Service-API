using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class Procedure
    {
        public int ProcedureId { get; set; }
        public int ProcedureMasterId { get; set; }
        public int PatientVisitId { get; set; }
        public bool IsDeprecated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ProcedureDescription { get; set; }
        public int PatientId { get; set; }
    }
}
