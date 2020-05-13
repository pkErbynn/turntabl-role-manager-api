using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Entities
{
    public class EmployeeRole
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
