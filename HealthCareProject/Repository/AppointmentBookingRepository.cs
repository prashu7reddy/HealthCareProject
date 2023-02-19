using HealthCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repository
{
    public class AppointmentBookingRepository : IRepository<AppointmentBooking>,IGetRepository<AppointmentBooking>
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
                AppointmentInDb.Date = obj.Date;
                AppointmentInDb.Time = obj.Time;
                AppointmentInDb.DocSpecializationId = obj.DocSpecializationId;
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

        public IEnumerable<AppointmentBooking> GetAll()
        {
            return _context.AppointmentBookings.ToList();
        }

        public async Task<AppointmentBooking> GetById(int id)
        {
            var appointment = await _context.AppointmentBookings.FindAsync(id);
            if(appointment!= null)
            {
                return appointment;
            }
            return null;
        }

       
    }
}
