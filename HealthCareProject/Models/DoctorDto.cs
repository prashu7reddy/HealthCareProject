using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public int DocSpecializationId { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
    }
}
