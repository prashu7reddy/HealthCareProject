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
        private readonly IGetAllAppointmentsByName<AppointmentBooking> _appointmentRepo;
        private readonly IGetRepository<AppointmentBookingDto> _appoinmentBookingRepository;
        //private readonly IGetAllAppointmentsByDoctorName<AppointmentBooking> _getAllAppointmentsByDoctorName;

        public AppointmentBookingController(IRepository<AppointmentBooking> repository, IGetRepository<AppointmentBookingDto> appoinmentBookingRepository, IGetAllAppointmentsByName<AppointmentBooking> appointmentRepo)
        {
            _repository = repository;
            _appoinmentBookingRepository = appoinmentBookingRepository;
             _appointmentRepo = appointmentRepo;
         //   _getAllAppointmentsByDoctorName = getAllAppointmentsByDoctorName;
        }
        //[Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet("GetAllBookings")]
        public IEnumerable<AppointmentBookingDto> GetAppointments()
        {
            return _appoinmentBookingRepository.GetAll();
        }

        [HttpGet]
        [Route("GetAppointmentById/{id}", Name = "GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appoinmentBookingRepository.GetById(id);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound();
        }

        //[HttpGet]
        //[Route("GetAppointmentbyId/{id}", Name = "GetAppointmentbyId")]
        //public async Task<IActionResult> GetAppointmentbyId(int id)
        //{
        //    var appointment = await _appoinmentBookingRepository.GetById(id);
        //    if (appointment != null)
        //    {
        //        return Ok(appointment);
        //    }
        //    return NotFound();
        //}



        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentBooking appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.create(appointment);
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, appointment);

        }


        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentBooking appointment)
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
            return Ok();
            
        }
        [HttpGet("GetAllAppointmentsByPatientName/{patientName}")]
        public async Task<IActionResult> GetAppointmentByPatientName(string patientName)
        {
            var appointment = await _appointmentRepo.GetAllAppointmentsByPatientName(patientName);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound();
        }
        [HttpGet("GetAllAppointmentsByDoctorName/{doctorName}")]
        public   async Task<IActionResult> GetAppointmentByDoctorName(string doctorName)
        {
            var appointment = await _appointmentRepo.GetAllAppointmentsByDoctorName(doctorName);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound();
        }


    }
}
