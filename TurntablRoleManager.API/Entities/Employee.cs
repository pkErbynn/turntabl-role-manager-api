﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string EmployeeFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string EmployeeLastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(60)]
        public string EmployeeEmail { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(255)]
        public string EmployeeAddress { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; }

    }
}
