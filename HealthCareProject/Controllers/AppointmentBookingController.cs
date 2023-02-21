using HealthCareProject.Models;
using HealthCareProject.Repository;
using Microsoft.AspNetCore.Authorization;
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
        //private readonly IAppRepository<AppointmentBooking> _appRepository;
        private readonly IGetRepository<AppointmentBookingDto> _appoinmentBookingRepository;

        public AppointmentBookingController(IRepository<AppointmentBooking> repository, IGetRepository<AppointmentBookingDto> appoinmentBookingRepository /*IAppRepository<AppointmentBooking> appRepository*/)
        {
            _repository = repository;
            _appoinmentBookingRepository = appoinmentBookingRepository;
           // _appRepository = appRepository;
        }
        //[Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet("GetAllBookings")]
               public IEnumerable<AppointmentBookingDto> GetAppointments()
        {
            return _appoinmentBookingRepository.GetAll();
        }
        [Authorize]
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

        //[Authorize(Roles = "Patient")]
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

       //[Authorize(Roles = "Patient")]
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

       // [Authorize(Roles = "Patient,Doctor")]
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
        //[HttpGet("GetAppByPatId/{id}")]
        //public async Task<IActionResult> GetAppByPatId(int id)
        //{
        //    var appointents = await _appRepository.GetAllAppByPatId(id);
        //    if (appointents != null)
        //    {
        //        return Ok(appointents);
        //    }
        //    return NotFound("No Appointments in this city");
        //}
    }
}
