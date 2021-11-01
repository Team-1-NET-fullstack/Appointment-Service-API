using System;
using System.Collections.Generic;

#nullable disable

namespace AppointmentScheduler.Service.API.Entities
{
    public partial class User
    {
        public User()
        {
            PatientVisitCreatedByNavigations = new HashSet<PatientVisit>();
            PatientVisitPhysicians = new HashSet<PatientVisit>();
            PatientVisitUpdatedByNavigations = new HashSet<PatientVisit>();
        }

        public int UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime Dob { get; set; }
        public int? EmployeeId { get; set; }
        public byte[] Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsFirstTimeUser { get; set; }
        public int? ContactNo { get; set; }
        public string Gender { get; set; }
        public int? WrongAttempts { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<PatientVisit> PatientVisitCreatedByNavigations { get; set; }
        public virtual ICollection<PatientVisit> PatientVisitPhysicians { get; set; }
        public virtual ICollection<PatientVisit> PatientVisitUpdatedByNavigations { get; set; }
    }
}
