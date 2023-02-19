using HealthCareProject.Models;
using HealthCareProject.Repository;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentBookingController : ControllerBase
    {
        private readonly IRepository<AppointmentBooking> _repository;
       private readonly IGetRepository<AppointmentBooking> _appoinmentBookingRepository;

        public AppointmentBookingController(IRepository<AppointmentBooking> repository, IGetRepository<AppointmentBooking> appoinmentBookingRepository)
        {
            _repository = repository;
            _appoinmentBookingRepository = appoinmentBookingRepository;
        }

        [HttpGet("GetAllBookings")]
        public IEnumerable<AppointmentBooking> GetAppointments()
        {
            return _appoinmentBookingRepository.GetAll();
        }

        [HttpGet]
        [Route("GetAppointmentById/{id}",Name ="GetAppointmentById")]
        public async Task<IActionResult>GetAppointmentById(int id)
        {
            var appointment = await _appoinmentBookingRepository.GetById(id);
            if(appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound();
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody]AppointmentBooking appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.create(appointment);
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, appointment);

        }
        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id,[FromBody]AppointmentBooking appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, appointment);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Appointment Not Found");
        }
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();

            }
            return NotFound("Appointment Not Found");
        }
    }
}
