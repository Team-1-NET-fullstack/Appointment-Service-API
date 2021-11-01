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
            var allAppointmentsData = _appointmentSchedulerService.GetAllAppointments();
            var jsonAllAppointmentsData = JsonConvert.SerializeObject(allAppointmentsData, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonAllAppointmentsData;
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<Object> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                var isCreated = await _appointmentSchedulerService.CreateAppointment(appointment);
                return isCreated != null ? true : false;
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
            var appointmentData = _appointmentSchedulerService.GetAppointmentByAppointmentId(appointmentId);
            var json = JsonConvert.SerializeObject(appointmentData, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }

        [HttpDelete]
        [Route("DeleteApointment/{appointmentId}")]
        public bool DeleteApointment(int appointmentId)
        {

            return _appointmentSchedulerService.DeleteApointment(appointmentId);


        }

        [HttpPut]
        [Route("AcceptAppointment/{appointmentId}")]
        public bool AcceptAppointment(int appointmentId, Appointment oldAppointment)
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
                    newAppointment.Status = true; // Accepting Appointment here
                    //newAppointment.IsActive = oldAppointment.IsActive;
                    //newAppointment.CreatedBy = oldAppointment.CreatedBy;
                    //newAppointment.CreatedDate = oldAppointment.CreatedDate;
                    newAppointment.UpdatedBy = oldAppointment.UpdatedBy;
                    newAppointment.UpdatedDate = DateTime.Now;

                    if (_appointmentSchedulerService.AcceptAppointment(newAppointment))
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

        //[HttpPut("{id}")]
        //public bool AcceptAppointment(int id, Appointment appointment)
        //{
        //    // var data = _appointmentSchedulerService.UpdateAppointment(id, appointment);
        //    // return data;
        //}
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
