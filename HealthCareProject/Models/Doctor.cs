using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        //navigation key
        public DocSpecialization Specialization { get; set; }

        public int DocSpecializationId { get; set; }

    }
}
