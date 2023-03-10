using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public class AppointmentBookingRepository : IRepository<AppointmentBooking>,IGetRepository<AppointmentBookingDto> ,IGetAllAppointmentsByName<AppointmentBooking>/*,IGetAllAppointmentsByDoctorName<AppointmentBooking>*/
    {
        private readonly ApplicationDbContext _context;
        public AppointmentBookingRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<AppointmentBooking> create(AppointmentBooking obj)
        {
            if (obj != null)
            {
                _context.AppointmentBookings.Add(obj);
                await _context.SaveChangesAsync();

            }
            return null;
        }
        public async Task<AppointmentBooking> Update(int id, AppointmentBooking obj)
        {
            var AppointmentInDb = await _context.AppointmentBookings.FindAsync(id);
            if (AppointmentInDb != null)
            {
                AppointmentInDb.PatientName = obj.PatientName;
                AppointmentInDb.Age = obj.Age;
                AppointmentInDb.HealthIssue = obj.HealthIssue;
                AppointmentInDb.PhoneNumber = obj.PhoneNumber;
                AppointmentInDb.Date = obj.Date;
                AppointmentInDb.Time = obj.Time;
                AppointmentInDb.DoctorName = obj.DoctorName;
                AppointmentInDb.Specialization = obj.Specialization;
                //AppointmentInDb.DocSpecializationId = obj.DocSpecializationId;
                _context.AppointmentBookings.Update(AppointmentInDb);
                await _context.SaveChangesAsync();
                return AppointmentInDb;
            }
            return null;
        }
        public async Task<AppointmentBooking> Delete(int id)
        {
            var AppointmentInDb = await _context.AppointmentBookings.FindAsync(id);
            if(AppointmentInDb != null)
            {
                _context.AppointmentBookings.Remove(AppointmentInDb);
                await  _context.SaveChangesAsync();
                return null;
            }
            return null;
        }

        public IEnumerable<AppointmentBookingDto> GetAll()
        {
            var Appointment = _context.AppointmentBookings.Include(m => m.Specialization).Select(x => new AppointmentBookingDto
            {
                Id = x.Id,
                PatientName=x.PatientName,
                Age=x.Age,
                HealthIssue=x.HealthIssue,
                PhoneNumber=x.PhoneNumber,
                Date = x.Date,
                Time = x.Time,
                DoctorName=x.DoctorName,
                SpecializationName = x.Specialization.SpecializationName
            }).ToList();
            return Appointment;
            
        }

        public async Task<AppointmentBookingDto> GetById(int id)
        {
            var appointments = await  _context.AppointmentBookings.Include(m => m.Specialization).Select(x => new AppointmentBookingDto
            {
                Id = x.Id,
                PatientName = x.PatientName,
                Age = x.Age,
                HealthIssue = x.HealthIssue,
                PhoneNumber = x.PhoneNumber,
                Date = x.Date,
                Time = x.Time,
                DoctorName=x.DoctorName,
                SpecializationName = x.Specialization.SpecializationName
            }).ToListAsync();

            var appointment = appointments.FirstOrDefault(x => x.Id == id);
            if (appointment!= null)
            {
                return appointment;
            }
            return null;
        }
        public async Task<IEnumerable<AppointmentBooking>> GetAllAppointmentsByPatientName(string PatientName)
        {
            var appointments = await _context.AppointmentBookings.Where(h => h.PatientName == PatientName).ToListAsync();
            // h.UserName == PatientName).ToListAsync();
            if (AppointmentBooking.Count > 0)
                return appointments;
            else
                return appointments;
        }

        public async Task<IEnumerable<AppointmentBooking>> GetAllAppointmentsByDoctorName(string DoctorName)
        {
            var appointments = await _context.AppointmentBookings.Where(h => h.DoctorName == DoctorName).ToListAsync();
            if (AppointmentBooking.Count > 0)
            {
                return appointments;
            }
            else
                return appointments;
        }
    }
}
