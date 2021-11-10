using AppointmentScheduler.Service.API.BAL.Interfaces;
using AppointmentScheduler.Service.API.DAL.Interfaces;
using AppointmentScheduler.Service.API.Entities;
using AppointmentScheduler.Service.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.BAL.Service
{
    public class AppointmentSchedulerService : IAppointmentSchedulerService
    {
        private readonly IAppointmentSchedulerRepository _appointment;

        public AppointmentSchedulerService(IAppointmentSchedulerRepository appointment)
        {
            _appointment = appointment;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            try
            {
                return await _appointment.Create(appointment);
            }
            catch (Exception exception)
            {
                // return exception.Message;
                return null;
            }
        }

        public IEnumerable<AppointmentModel> GetAllAppointments()
        {
            try
            {
                return _appointment.GetAll().ToList();
            }
            catch (Exception exception)
            {
                // return exception.Message;
                throw;
            }
        }

        public IEnumerable<UserModel> GetAllUsersForLogin()
        {
            try
            {
                return _appointment.GetAllUsersForLogin().ToList();
            }
            catch (Exception exception)
            {
                // return exception.Message;
                throw;
            }
        }

        public IEnumerable<UserModel> GetAllUsers(int roleId)
        {
            try
            {
                return _appointment.GetAllUsers(roleId).ToList();
            }
            catch (Exception exception)
            {
                // return exception.Message;
                throw;
            }
        }

        public Appointment GetEntityByAppointmentId(int Id)
        {
            try
            {
                return _appointment.GetEntityById(Id);
            }
            catch (Exception exception)
            {
                // return exception.Message;
                throw;
            }
        }

        public AppointmentModel GetAppointmentByAppointmentId(int Id)
        {
            try
            {
                return _appointment.GetById(Id);
            }
            catch (Exception exception)
            {
                // return exception.Message;
                throw;
            }
        }

        public bool DeleteApointment(int Id)
        {
            var DataList = _appointment.GetAllEntities().Where(x => x.AppointmentId == Id).ToList();
            foreach (var item in DataList)
            {
                _appointment.Delete(item);
            }
            return true;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            _appointment.Update(appointment);
            return true;
        }

        public IEnumerable<AppointmentModel> GetAllAvailablePhysicians(DateTime date)
        {
            return _appointment.GetAllAvailablePhysicians(date);
        }
    }
}
