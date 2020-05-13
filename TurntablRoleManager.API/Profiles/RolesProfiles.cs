using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurntablRoleManager.API.Profiles
{
    public class RolesProfiles: Profile
    {
        public RolesProfiles()
        {
            CreateMap<Entities.Role, Models.RoleTo>();
        }
    }
}
