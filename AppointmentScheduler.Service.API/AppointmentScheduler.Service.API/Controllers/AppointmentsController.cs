using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentScheduler.Service.API.Entities;
using AppointmentScheduler.Service.API.BAL.Service;
using Newtonsoft.Json;
using AppointmentScheduler.Service.API.Models;
using System.Data;
using System.Globalization;

namespace AppointmentScheduler.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentSchedulerService _appointmentSchedulerService;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public AppointmentsController(AppointmentSchedulerService appointmentSchedulerService)
        {
            _appointmentSchedulerService = appointmentSchedulerService;

        }

        [HttpGet]
        [Route("GetAllAppointments")]
        public Object GetAllAppointments()
        {
            try
            {
                var allAppointmentsData = _appointmentSchedulerService.GetAllAppointments();
                var jsonAllAppointmentsData = JsonConvert.SerializeObject(allAppointmentsData, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                return jsonAllAppointmentsData;
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }

        [HttpGet]
        [Route("GetAllUsersForLogin")]
        public Object GetAllUsersForLogin()
        {
            try
            {
                var allAppointmentsData = _appointmentSchedulerService.GetAllUsersForLogin();
                var jsonAllAppointmentsData = JsonConvert.SerializeObject(allAppointmentsData, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                return jsonAllAppointmentsData;
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public Object GetAllUsers(int roleId)
        {
            try
            {
                var allAppointmentsData = _appointmentSchedulerService.GetAllUsers(roleId);
                var jsonAllAppointmentsData = JsonConvert.SerializeObject(allAppointmentsData, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                return jsonAllAppointmentsData;
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<Object> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                var varAppointment = new Appointment();
                try
                {
                    varAppointment.Title = appointment.Title;
                    varAppointment.Reason = appointment.Reason;
                    varAppointment.StartTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, INDIAN_ZONE);
                    varAppointment.EndTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.EndTime, INDIAN_ZONE);
                    varAppointment.Status = appointment.Status;
                    varAppointment.CreatedBy = appointment.PatientId;
                    varAppointment.CreatedDate = DateTime.Now;
                    varAppointment.PhysicianId = appointment.PhysicianId;
                    varAppointment.PatientId = appointment.PatientId;
                    varAppointment.ModifiedBy = appointment.ModifiedBy;
                    varAppointment.ModifiedDate = DateTime.Now;
                    varAppointment.EmployeeId = appointment.EmployeeId;
                    varAppointment.IsActive = true;

                    return await _appointmentSchedulerService.CreateAppointment(varAppointment);
                }
                catch (Exception e)
                {
                    return (e.Message);
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        [HttpGet]
        [Route("GetAppointmentByAppointmentId/{appointmentId}")]
        public Object GetAppointmentByAppointmentId(int appointmentId)
        {
            try
            {
                var appointmentData = _appointmentSchedulerService.GetAppointmentByAppointmentId(appointmentId);
                var json = JsonConvert.SerializeObject(appointmentData, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
                return json;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        [HttpDelete]
        [Route("DeleteApointment/{appointmentId}")]
        public bool DeleteApointment(int appointmentId)
        {
            try
            {
                return _appointmentSchedulerService.DeleteApointment(appointmentId);
            }
            catch (Exception exception)
            {
                // exception.Message;
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateAppointment")]
        public bool UpdateAppointment(Appointment oldAppointment)
        {
            var appointmentFromRepo = _appointmentSchedulerService.GetEntityByAppointmentId(oldAppointment.AppointmentId);

            if (ModelState.IsValid)
            {
                var newAppointment = new Appointment();
                newAppointment = appointmentFromRepo;
                try
                {
                    newAppointment.AppointmentId = oldAppointment.AppointmentId;
                    newAppointment.Title = oldAppointment.Title;
                    newAppointment.Reason = oldAppointment.Reason;
                    newAppointment.Status = oldAppointment.Status;
                    newAppointment.IsActive = true;

                    if (_appointmentSchedulerService.UpdateAppointment(newAppointment))
                        return true;
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    // return exception.Message;
                    return false;
                }
            }
            else
                return false;
        }

        [HttpPut]
        [Route("UpdateAppointmentStatus")]
        public bool UpdateAppointmentStatus(Appointment oldAppointment)
        {
            var appointmentFromRepo = _appointmentSchedulerService.GetEntityByAppointmentId(oldAppointment.AppointmentId);

            if (ModelState.IsValid)
            {
                var newAppointment = new Appointment();
                newAppointment = appointmentFromRepo;
                try
                {
                    newAppointment.AppointmentId = oldAppointment.AppointmentId;
                    newAppointment.Status = oldAppointment.Status;
                    newAppointment.ModifiedDate = DateTime.Now;

                    if (_appointmentSchedulerService.UpdateAppointment(newAppointment))
                        return true;
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    // return exception.Message;
                    return false;
                }
            }
            else
                return false;
        }

        [HttpPut]
        [Route("AcceptAppointment")]
        public bool AcceptAppointment(Appointment oldAppointment)
        {
            var appointmentFromRepo = _appointmentSchedulerService.GetEntityByAppointmentId(oldAppointment.AppointmentId);

            if (ModelState.IsValid)
            {
                var newAppointment = new Appointment();
                newAppointment = appointmentFromRepo;
                try
                {
                    newAppointment.AppointmentId = oldAppointment.AppointmentId;
                    newAppointment.Status = "Active";
                    newAppointment.ModifiedBy = oldAppointment.ModifiedBy;
                    newAppointment.ModifiedDate = DateTime.Now;

                    if (_appointmentSchedulerService.UpdateAppointment(newAppointment))
                        return true;
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    // return exception.Message;
                    return false;
                }
            }
            else
                return false;
        }

        [HttpPut]
        [Route("RejectAppointment/{appointmentId}")]
        public bool RejectAppointment(int appointmentId, Appointment oldAppointment)
        {
            var appointmentFromRepo = _appointmentSchedulerService.GetEntityByAppointmentId(appointmentId);

            if (ModelState.IsValid)
            {
                var newAppointment = new Appointment();
                newAppointment = appointmentFromRepo;
                try
                {
                    newAppointment.AppointmentId = appointmentId;
                    newAppointment.Status = "Declined";
                    newAppointment.ModifiedBy = oldAppointment.ModifiedBy;
                    newAppointment.ModifiedDate = DateTime.Now;

                    if (_appointmentSchedulerService.UpdateAppointment(newAppointment))
                        return true;
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    // return exception.Message;
                    return false;
                }
            }
            else
                return false;
        }

        [HttpGet]
        [Route("getAllAvailablePhysician")]
        public ActionResult<IEnumerable<AppointmentModel>> GetAllAvailablePhysicians(string startDate)
        {
            try
            {
                DateTime date = DateTime.ParseExact(startDate.Substring(0, 24), "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var allPhysicians = _appointmentSchedulerService.GetAllAvailablePhysicians(date);
                return Ok(allPhysicians);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
