using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public class PatientRepository : IRepository<Patient>,IGetRepository<Patient>
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      

        public async Task<Patient> create(Patient obj)
        {
            if (obj != null)
            {
                _context.Patients.Add(obj);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<Patient> Delete(int id)
        {
            var patientInDb = await _context.Patients.FindAsync(id);
            if (patientInDb != null)
            {
                _context.Patients.Remove(patientInDb);
                await _context.SaveChangesAsync();
                return patientInDb;
            }
            return null;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

      

        public async Task<Patient> GetById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                return patient;
            }
            return null;
        }

        public async Task<Patient> Update(int id, Patient obj)
        {
            var patientInDb = await _context.Patients.FindAsync(id);
            if (patientInDb != null)
            {
                patientInDb.PatientName = obj.PatientName;
                patientInDb.Age = obj.Age;
                patientInDb.Email = obj.Email;  
                patientInDb.PhoneNumber = obj.PhoneNumber;
                patientInDb.Address = obj.Address;
                //patientInDb.Entry(doctorInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Patients.Update(patientInDb);
                await _context.SaveChangesAsync();
                return patientInDb;
            }
            return null;
        }
    }
}
