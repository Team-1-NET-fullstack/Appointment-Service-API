using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.Models
{
    public class UserModel
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public byte[]? password { get; set; }
        public DateTime dob { get; set; }
        public int? employeeId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public int roleId { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
        public bool? isActive { get; set; }
        public bool? isBlocked { get; set; }
        public bool? isFirstTimeUser { get; set; }
        public int? contactNo { get; set; }
        public string gender { get; set; }
        public int? noOfWrongAttempts { get; set; }
    }
}
