﻿using HealthCareProject.Models;
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
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IDoctorRepository _DoctorRepository;
        public DoctorsController(IRepository<Doctor> repository,IDoctorRepository doctorRepository)
        {
            _repository = repository;
            _DoctorRepository = doctorRepository;
        }

        [HttpGet("GetAllDoctors")]
        
        public IEnumerable<Doctor> GetDoctors()
        {
            return _repository.GetAll();
        }
        [HttpGet]
        [Route("GetDoctorById/{id}", Name ="GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _repository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);

            }
            return NotFound();

        }


        [HttpGet("SearchDoctor/{specialization}")]
        public async Task<IActionResult> SearchDoctorBySpecialization(string specialization)
        {
           var result= await _DoctorRepository.SearchBySpecialization(specialization);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("please provide valid specialization");
        }


        [HttpPost("CreateDoctor")]

        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _repository.create(doctor);
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id },doctor);
        }
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id,[FromBody]Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor Not Found");

        }
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if(result != null)
            {
                return Ok();

            }
            return NotFound("Doctor Not Found");
        }


    }
}