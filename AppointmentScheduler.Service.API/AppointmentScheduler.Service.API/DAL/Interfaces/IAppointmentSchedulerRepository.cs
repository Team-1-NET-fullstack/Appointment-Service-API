using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentScheduler.Service.API.Entities;
using AppointmentScheduler.Service.API.Models;

namespace AppointmentScheduler.Service.API.DAL.Interfaces
{
    public interface IAppointmentSchedulerRepository
    {
        public Task<Appointment> Create(Appointment _object);
        public void Update(Appointment _object);
        public IEnumerable<Appointment> GetAllEntities();
        public IEnumerable<AppointmentModel> GetAll();
        public Appointment GetEntityById(int Id);
        public AppointmentModel GetById(int Id);
        public bool Delete(Appointment _object);
    }
}