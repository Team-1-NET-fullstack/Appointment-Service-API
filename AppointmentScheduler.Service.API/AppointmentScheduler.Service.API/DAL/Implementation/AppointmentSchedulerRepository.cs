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

        public IEnumerable<UserModel> GetAllUsers(int roleId)
        {
            var list = (from u in _context.Users
                        where u.RoleId == roleId
                        select new UserModel
                        {
                            Title = u.Title,
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            EmployeeId = (int)u.EmployeeId,
                            RoleId = u.RoleId,
                            IsActive = u.IsActive,
                            IsBlocked = u.IsBlocked,
                            CreatedDate = u.CreatedDate
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
                // var obj = GetEntityById(_object.AppointmentId);

                _context.Entry(_object).State = EntityState.Modified;

                // obj;

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

        //public Task<AppointmentModel> Create(AppointmentModel _object)
        //{
        //    var isCreated = false;

        //    try
        //    {
        //        var appointment = new Appointment
        //        {
        //            AppointmentId = (int)_object.AppointmentId,
        //            PatientId = (int)_object.PatientId,
        //            PhysicianId = (int)_object.PhysicianId,
        //            Reason = _object.Reason,
        //            TimeSlot = _object.TimeSlot,
        //            Duration = (int)_object.Duration,
        //            Status = (bool)_object.Status,
        //            CreatedBy = (int)_object.CreatedBy,
        //            CreatedDate = (DateTime)_object.CreatedDate,
        //            UpdatedBy = (int)_object.UpdatedBy,
        //            UpdatedDate = (DateTime)_object.UpdatedDate,
        //        };
        //        _context.Appointments.AddAsync(appointment);
        //        _context.SaveChangesAsync();

        //        var Physician = _context.Users.FirstOrDefault(a => a.UserId == e.PhysicianId);
        //        var patient = _context.Users.FirstOrDefault(a => a.UserId == e.PatientId);
        //        isCreated = true;
        //        return isCreated;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Update(int Id, AppointmentModel _object)
        //{
        //    if (Id != _object.AppointmentId)
        //    {
        //        return false;
        //    }

        //    _context.Entry(_object).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException exception)
        //    {
        //        if (!AppointmentExists(Id))
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return true;
        //}

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}