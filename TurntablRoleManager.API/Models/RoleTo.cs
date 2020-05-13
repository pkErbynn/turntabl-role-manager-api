using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Models
{
    public class RoleTo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
