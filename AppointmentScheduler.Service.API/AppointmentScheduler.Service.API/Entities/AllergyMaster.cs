using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class AllergyMaster
    {
        public int AllergyMastersId { get; set; }
        public string Description { get; set; }
        public bool IsFatal { get; set; }
    }
}
