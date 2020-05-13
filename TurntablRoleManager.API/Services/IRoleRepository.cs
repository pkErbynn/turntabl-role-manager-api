
using System;
using System.Collections.Generic;
using TurntablRoleManager.API.Entities;
using TurntablRoleManager.API.Models;

namespace TurntablRoleManager.API.Services
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(Guid roleId);
        Role AddRole(Role role);
        Role UpdateRole(Role role);
        bool RoleExists(Guid roleId);
        void DeleteRole(Guid roleId);
    }
}
