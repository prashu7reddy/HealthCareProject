using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //add the table reference
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<DocSpecialization> DocSpecializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<AppointmentBooking> AppointmentBookings { get; set; }
        //admin part

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<UserRoles> Roles { get; set; }

        public DbSet<UserRoleMappings> UserRoleMpping { get; set; }


    }
}
