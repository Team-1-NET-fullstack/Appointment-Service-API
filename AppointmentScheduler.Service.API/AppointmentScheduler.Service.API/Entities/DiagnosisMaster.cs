using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class DiagnosisMaster
    {
        public int DiagnosisMastersId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
