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
   
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> _repository;
        private readonly IGetRepository<Patient> _PatientRepository;
       

        public PatientController(IRepository<Patient> repository,IGetRepository<Patient> getRepository)
        {
            _repository = repository;
            _PatientRepository = getRepository;
          
        }

        [HttpGet("GetAllPatients")]

        public IEnumerable<Patient> GetPatients()
        {
            return _PatientRepository.GetAll();
        }
        [HttpGet]
        [Route("GetPatientById/{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _PatientRepository.GetById(id);
            if (patient != null)
            {
                return Ok(patient);

            }
            return NotFound();

        }

        [HttpPost("CreatePatient")]

        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            await _repository.create(patient);
            return CreatedAtRoute("GetPatientById", new { id = patient.Id }, patient);
        }
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, patient);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Patient Not Found");

        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();

            }
            return NotFound("Patient Not Found");
        }
      
       
    }
}
