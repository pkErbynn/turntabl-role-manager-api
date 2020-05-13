using TurntablRoleManager.API.DbContexts;
using TurntablRoleManager.API.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;

namespace TurntablRoleManager.API.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TurntablDbContext _context;

        public RoleRepository(TurntablDbContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.OrderByDescending(x => x.CreatedAt).ToList<Role>();
        }

        public Role GetRoleById(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            return _context.Roles.FirstOrDefault(a => a.Id == roleId);
        }

        public Role AddRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            // the repository fills the id (instead of using identity columns)
            role.Id = Guid.NewGuid();
            role.CreatedAt = DateTime.UtcNow;

            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }

        public bool RoleExists(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            return _context.Roles.Any(a => a.Id == roleId);
        }

        public Role UpdateRole(Role role)
        {
            var foundRole = _context.Roles.FirstOrDefault(e => e.Id == role.Id);
            if (foundRole != null)
            {
                foundRole.Name = role.Name;
                foundRole.Description = role.Description;

                _context.SaveChanges();

                return foundRole;
            }

            return null;
        }

        public void DeleteRole(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            var foundRole = _context.Roles.FirstOrDefault(e => e.Id == roleId);
            if (foundRole == null) return;

            _context.Roles.Remove(foundRole);
            _context.SaveChanges();
        }
    }
}
