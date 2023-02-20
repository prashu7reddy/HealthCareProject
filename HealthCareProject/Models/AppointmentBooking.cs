using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class AppointmentBooking
    {
        public int Id { get; set; }
        public DateTime Date{get;set;}
        public DateTime Time{get;set;}

        public DocSpecialization Specialization { get; set; }
        
        public int DocSpecializationId { get; set; }
    }
}
