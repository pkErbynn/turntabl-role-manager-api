using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; }

    }
}
