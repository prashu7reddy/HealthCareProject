using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //add the table reference
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DocSpecialization> DocSpecializations { get; set; }
    }
}
