using AppointmentScheduler.Service.API.DAL.Interfaces;
using AppointmentScheduler.Service.API.Entities;
using AppointmentScheduler.Service.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Service.API.DAL.Implementation
{
    public class AppointmentSchedulerRepository : IAppointmentSchedulerRepository
    {
        private readonly CTGeneralHospitalContext _context;

        public AppointmentSchedulerRepository(CTGeneralHospitalContext context)
        {
            _context = context;
        }

        public async Task<Appointment> Create(Appointment _object)
        {
            try
            {
                var appointmentObject = await _context.Appointments.AddAsync(_object);
                _context.SaveChanges();
                return appointmentObject.Entity;
            }
            catch (Exception exception)
            {
                // return exception.Message;
                return null;
            }
        }

        public bool Delete(Appointment _object)
        {
            try
            {
                _context.Remove(_object);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                // return exception.Message;
                return false;
            }
        }

        public IEnumerable<Appointment> GetAllEntities()
        {
            var appointmentsList = _context.Appointments.ToList();
            return appointmentsList;
        }

        public IEnumerable<AppointmentModel> GetAll()
        {
            var appointmentsList = _context.Appointments
                .Select(e => new AppointmentModel
                {
                    appointmentId = e.AppointmentId,
                    patientId = e.PatientId,
                    physicianId = e.PhysicianId,
                    employeeId = e.EmployeeId, // _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    title = e.Title,
                    startTime = e.StartTime,
                    endTime = e.EndTime,
                    status = e.Status,
                    reason = e.Reason,
                    isActive = e.IsActive,
                    createdBy = e.CreatedBy,
                    modifiedBy = e.ModifiedBy,
                    createdDate = e.CreatedDate,
                    modifiedDate = e.ModifiedDate,
                    physicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    patientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                }).ToList();

            return appointmentsList;
        }

        public IEnumerable<UserModel> GetAllUsersForLogin()
        {
            var list = (from u in _context.Users
                        select new UserModel
                        {
                            title = u.Title,
                            userId = u.UserId,
                            firstName = u.FirstName,
                            lastName = u.LastName,
                            emailId = u.EmailId,
                            password = u.Password,
                            employeeId = (int)u.EmployeeId,
                            roleId = u.RoleId,
                            isActive = u.IsActive,
                            isBlocked = u.IsBlocked,
                            createdDate = u.CreatedDate
                        }).ToList();
            return list;
        }

        public IEnumerable<UserModel> GetAllUsers(int roleId)
        {
            var list = (from u in _context.Users
                        where u.RoleId == roleId
                        select new UserModel
                        {
                            title = u.Title,
                            userId = u.UserId,
                            firstName = u.FirstName,
                            lastName = u.LastName,
                            emailId = u.EmailId,
                            employeeId = (int)u.EmployeeId,
                            roleId = u.RoleId,
                            isActive = u.IsActive,
                            isBlocked = u.IsBlocked,
                            createdDate = u.CreatedDate
                        }).ToList();
            return list;
        }


        public Appointment GetEntityById(int Id)
        {
            {
                var appointmentData = _context.Appointments.Where(s => s.AppointmentId == Id).FirstOrDefault();
                return appointmentData;
            }
        }

        public AppointmentModel GetById(int Id)
        {
            var appointmentData = _context.Appointments
                .Select(e => new AppointmentModel
                {
                    appointmentId = e.AppointmentId,
                    patientId = e.PatientId,
                    physicianId = e.PhysicianId,
                    reason = e.Reason,
                    status = e.Status,
                    isActive = e.IsActive,
                    createdBy = e.CreatedBy,
                    createdDate = e.CreatedDate,
                    modifiedBy = e.ModifiedBy,
                    modifiedDate = e.ModifiedDate,
                    physicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    patientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    employeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                }).Where(s => s.appointmentId == Id).FirstOrDefault();

            return appointmentData;
        }

        public void Update(Appointment _object)
        {
            try
            {
                _context.Entry(_object).State = EntityState.Modified;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!AppointmentExists(_object.AppointmentId))
                {
                    // return false;
                }
                else
                {
                    throw;
                }
            }

            // return true;
        }

        public IEnumerable<AppointmentModel> GetAllAvailablePhysicians(DateTime startTime)
        {
            var appointments = (from a in _context.Appointments
                                where DateTime.Compare(a.StartTime, (startTime)) == 0 & a.IsActive == true
                                select a).ToList();

            var allPhysicians = (from u in _context.Users
                                 where u.RoleId == 2
                                 select new AppointmentModel
                                 {
                                     physicianId = u.UserId,
                                     physicianName = u.FirstName + ' ' + u.LastName
                                 }).ToList();

            foreach (var a in appointments)
            {
                allPhysicians.Remove(allPhysicians.SingleOrDefault(x => x.physicianId == a.PhysicianId));
            }

            return allPhysicians;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}