using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public DateTime Slots { get; set; }
    }
}
