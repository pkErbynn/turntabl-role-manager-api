using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Models
{
    public class AddEmployeeDTO
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }

        public List<string> RoleGuids { get; set; }
    }
}
