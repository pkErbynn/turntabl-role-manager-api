using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntablRoleManager.API.Entities;
using TurntablRoleManager.API.Models;
using TurntablRoleManager.API.Services;

namespace TurntablRoleManager.API.Controllers
{   
    [ApiController]
    [Route("api/roles")]
    public class RoleController: ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleTo>> GetAllRoles()
        {
            var rolesObjects = _roleRepository.GetAllRoles();
            return Ok(_mapper.Map<IEnumerable<RoleTo>>(rolesObjects));
        }

        [HttpGet("{roleId}")]
        public IActionResult GetRole(Guid roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoleTo>(role));
        }

        [HttpPost]
        public ActionResult<RoleTo> CreateRole([FromBody] Role role)
        {
            if (role == null) { return BadRequest(); }
         
            if (role.Name == null || role.Description == null)
            {
                ModelState.AddModelError("Name/Description", "The name or description shouldn't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdObject = _roleRepository.AddRole(role);
            var createdRole = _mapper.Map<RoleTo>(createdObject);

            return Created("role", createdRole);
        }

        [HttpPut]
        public ActionResult UpdateRole([FromBody] Role role)
        {
            if (role == null) { return BadRequest(); }

            if (role.Name == null || role.Description == null)
            {
                ModelState.AddModelError("Name/Description", "The name or description shouldn't be empty");
            }

            if (!ModelState.IsValid){ return BadRequest(ModelState); }

            var roleToUpdate = _roleRepository.RoleExists(role.Id);

            if (!roleToUpdate) { return NotFound(); }
                
            _roleRepository.UpdateRole(role);
            return Ok(); //success
        }
    }
}
