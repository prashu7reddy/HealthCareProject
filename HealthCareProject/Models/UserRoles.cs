﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Models
{
    public class UserRoles
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
