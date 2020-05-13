using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Models
{
    public class UpdateEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
