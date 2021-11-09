using AppointmentScheduler.Service.API.Entities;
using AppointmentScheduler.Service.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.BAL.Interfaces
{
    interface IAppointmentSchedulerService
    {
        public IEnumerable<AppointmentModel> GetAllAppointments();
        public Task<Appointment> CreateAppointment(Appointment appointment);
        public Appointment GetEntityByAppointmentId(int Id);
        public AppointmentModel GetAppointmentByAppointmentId(int Id);
        public bool DeleteApointment(int Id);
        public bool UpdateAppointment(Appointment appointment);
    }
}
