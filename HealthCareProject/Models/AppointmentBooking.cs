using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class AppointmentBooking
    {
        public int Id { get; set; }
        //public Patient Patient { get; set; }
        //public int PatientId { get; set; }
      
        public string PatientName { get; set; }
        public int Age { get; set; }

        public string HealthIssue { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date{get;set;}
        public DateTime Time{get;set;}

        public DocSpecialization Specialization { get; set; }
        
        public int DocSpecializationId { get; set; }
    }
}
