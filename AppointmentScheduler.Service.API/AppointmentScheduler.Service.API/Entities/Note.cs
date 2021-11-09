using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class Note
    {
        public int NoteId { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public bool IsUrgent { get; set; }
        public string Message { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
