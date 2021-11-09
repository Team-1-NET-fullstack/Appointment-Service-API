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

namespace AppointmentScheduler.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentSchedulerService _appointmentSchedulerService;

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

                return await _appointmentSchedulerService.CreateAppointment(appointment);

                // var isCreated = await _appointmentSchedulerService.CreateAppointment(appointment);
                // return isCreated != null;
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
                    //newAppointment.PatientId = oldAppointment.PatientId;
                    //newAppointment.PhysicianId = oldAppointment.PhysicianId;
                    //newAppointment.Reason = oldAppointment.Reason;
                    //newAppointment.TimeSlot = oldAppointment.TimeSlot;
                    //newAppointment.Duration = oldAppointment.Duration;
                    newAppointment.Status = "Active"; // Accepting Appointment here
                    //newAppointment.IsActive = oldAppointment.IsActive;
                    //newAppointment.CreatedBy = oldAppointment.CreatedBy;
                    //newAppointment.CreatedDate = oldAppointment.CreatedDate;
                    newAppointment.ModifiedBy = oldAppointment.ModifiedBy;
                    newAppointment.ModifiedDate = DateTime.Now;
                    // newAppointment.SlotDate = DateTime.Now;

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
                    //newAppointment.PatientId = oldAppointment.PatientId;
                    //newAppointment.PhysicianId = oldAppointment.PhysicianId;
                    //newAppointment.Reason = oldAppointment.Reason;
                    //newAppointment.TimeSlot = oldAppointment.TimeSlot;
                    //newAppointment.Duration = oldAppointment.Duration;
                    newAppointment.Status = "Declined"; // Accepting Appointment here
                    //newAppointment.IsActive = oldAppointment.IsActive;
                    //newAppointment.CreatedBy = oldAppointment.CreatedBy;
                    //newAppointment.CreatedDate = oldAppointment.CreatedDate;
                    newAppointment.ModifiedBy = oldAppointment.ModifiedBy;
                    newAppointment.ModifiedDate = DateTime.Now;
                    // newAppointment.SlotDate = DateTime.Now;

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
    }

    //[HttpPut("{id}")]
    //public async Task<ActionResult<AppointmentModel>> RejectAppointment(int id)
    //{

    //    var data = await _appointmentSchedulerService.CreateAppointment(appointment);
    //    return CreatedAtAction("GetAllAppointments", new { id = appointment.AppointmentId }, appointment);
    //}

    //// GET: api/Appointments/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Appointment>> GetAppointment(int id)
    //{
    //    var appointment = await _context.Appointments.FindAsync(id);

    //    if (appointment == null)
    //    {
    //        return NotFound();
    //    }

    //    return appointment;
    //}

    //// PUT: api/Appointments/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
    //{
    //    if (id != appointment.AppointmentId)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(appointment).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!AppointmentExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}



    //// DELETE: api/Appointments/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteAppointment(int id)
    //{
    //    var appointment = await _context.Appointments.FindAsync(id);
    //    if (appointment == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Appointments.Remove(appointment);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool AppointmentExists(int id)
    //{
    //    return _context.Appointments.Any(e => e.AppointmentId == id);
    //}

}
