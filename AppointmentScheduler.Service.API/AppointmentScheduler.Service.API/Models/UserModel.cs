using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime Dob { get; set; }
        public int? EmployeeId { get; set; }
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
        public int? NoOfWrongAttempts { get; set; }
    }
}
