﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

       

    }
}
