using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public class DoctorRepository : IRepository<Doctor>,IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Doctor> create(Doctor obj)
        {
            if (obj != null)
            {
                _context.Doctors.Add(obj);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var doctorInDb = await _context.Doctors.FindAsync(id);
            if (doctorInDb != null)
            {
                doctorInDb.DoctorName = obj.DoctorName;
                doctorInDb.DocSpecializationId = obj.DocSpecializationId;
                //_context.Entry(doctorInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Doctors.Update(doctorInDb);
                await _context.SaveChangesAsync();
                return doctorInDb;
            }
            return null;
        }

        public async Task<Doctor> Delete(int id)
        {
            var doctorInDb = await _context.Doctors.FindAsync(id);
            if (doctorInDb != null)
            {
                _context.Doctors.Remove(doctorInDb);
                await _context.SaveChangesAsync();
                return doctorInDb;
            }
            return null; 
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public async Task<Doctor> GetById(int id)
        {
           var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        public async Task<IEnumerable<Doctor>> SearchBySpecialization(string specialization)
        {
            if (!string.IsNullOrWhiteSpace(specialization))
            {
                var doctors = await _context.Doctors.Include(x => x.Specialization).Where(x => x.Specialization.SpecializationName.Contains(specialization)).ToListAsync();
                return doctors;
            }
            return null;
        }
    }
}
