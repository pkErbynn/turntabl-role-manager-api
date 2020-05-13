using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TurntablRoleManager.API.DbContexts;
using TurntablRoleManager.API.Entities;
using TurntablRoleManager.API.Models;

namespace TurntablRoleManager.API.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TurntablDbContext _context;

        public EmployeeRepository(TurntablDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all employees and their corresponding roles 
        public IEnumerable<DetailEmployeeDTO> GetEmployees()
        {
            List<DetailEmployeeDTO> detailEmployeeDTOs = new List<DetailEmployeeDTO>();

            var employeesInDb = _context.Employees.ToList();

            foreach (var emp in employeesInDb)
            {
                // fetching roles related to each employee
                var rolesInDb = (from e in _context.Employees
                                 join er in _context.EmployeeRoles on e.EmployeeId equals er.EmployeeId
                                 join r in _context.Roles on er.Id equals r.Id
                                 where e.EmployeeId == emp.EmployeeId
                                 select r).ToList();

                List<RoleTo> individualEmployeeRoles = new List<RoleTo>();

                foreach (var r in rolesInDb)
                {
                    // mapping role dto
                    RoleTo roleTo = new RoleTo();
                    roleTo.Id = r.Id;
                    roleTo.Name = r.Name;
                    roleTo.Description = r.Description;
                    roleTo.CreatedAt = r.CreatedAt;

                    individualEmployeeRoles.Add(roleTo);
                }

                // mapping employee dto to corresponding fields 
                DetailEmployeeDTO detailEmployeeDTO = new DetailEmployeeDTO();
                detailEmployeeDTO.EmployeeId = emp.EmployeeId;
                detailEmployeeDTO.EmployeeFirstName = emp.EmployeeFirstName;
                detailEmployeeDTO.EmployeeLastName = emp.EmployeeLastName;
                detailEmployeeDTO.EmployeeEmail = emp.EmployeeEmail;
                detailEmployeeDTO.EmployeeAddress = emp.EmployeeAddress;
                detailEmployeeDTO.Roles = individualEmployeeRoles;

                detailEmployeeDTOs.Add(detailEmployeeDTO);
            };

            return detailEmployeeDTOs;
        }


        // Get single employee and their roles 
        public DetailEmployeeDTO GetEmployee(int id)
        {
            DetailEmployeeDTO detailEmployee = new DetailEmployeeDTO();
            List<RoleTo> soloEmployeeRoles = new List<RoleTo>();

            var querableEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            // fetching roles related to employee
            var querableRoles = (from e in _context.Employees
                                 join er in _context.EmployeeRoles on e.EmployeeId equals er.EmployeeId
                                 join r in _context.Roles on er.Id equals r.Id
                                 where e.EmployeeId == querableEmployee.EmployeeId
                                 select r).ToList();

            // fetching all roles related to the employee
            foreach (var r in querableRoles)
            {
                RoleTo roleTo = new RoleTo();
                roleTo.Id = r.Id;
                roleTo.Name = r.Name;
                roleTo.Description = r.Description;
                roleTo.CreatedAt = r.CreatedAt;

                soloEmployeeRoles.Add(roleTo);
            }

            // mapping querable data to employee detail 
            detailEmployee.EmployeeId = querableEmployee.EmployeeId;
            detailEmployee.EmployeeFirstName = querableEmployee.EmployeeFirstName;
            detailEmployee.EmployeeLastName = querableEmployee.EmployeeLastName;
            detailEmployee.EmployeeEmail = querableEmployee.EmployeeEmail;
            detailEmployee.EmployeeAddress = querableEmployee.EmployeeAddress;
            detailEmployee.Roles = soloEmployeeRoles;

            return detailEmployee;
        }

        // Assign roles during employee creation
        public EmployeeTo AssignEmployeeWithRoles(AddEmployeeDTO employeeDTO)
        {   
            // mapping dto to an employee
            Employee employee = new Employee();
            employee.EmployeeFirstName = employeeDTO.EmployeeFirstName;
            employee.EmployeeLastName = employeeDTO.EmployeeLastName;
            employee.EmployeeEmail = employeeDTO.EmployeeEmail;
            employee.EmployeeAddress = employeeDTO.EmployeeAddress;

            // saving employee part of dto to db
            _context.Employees.Add(employee);
            _context.SaveChanges();

            // convert dto role string guids to pure guids
            List<Guid> roleGuids = new List<Guid>();
            foreach (var stringGuid in employeeDTO.RoleGuids)
            {
                Guid roleGuid = Guid.Parse(stringGuid);
                roleGuids.Add(roleGuid);
            }

            // assigning employee with roles and saving to db
            EmployeeRole employeeRole = new EmployeeRole();
            foreach (var guid in roleGuids)
            {
                employeeRole.Id = guid;     // roleId
                employeeRole.EmployeeId = employee.EmployeeId;

                _context.EmployeeRoles.Add(employeeRole);
                _context.SaveChanges();
            }

            EmployeeTo employeeTo = new EmployeeTo()
            {
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeeLastName = employee.EmployeeLastName,
                EmployeeEmail = employee.EmployeeEmail,
                EmployeeAddress = employee.EmployeeAddress
            };

            return employeeTo;
        }

        // Remove employee by their Id and their assigned roles
        public int DeleteEmployee(int id)
        {
            var querableEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if(querableEmployee == null)
            {
                Console.WriteLine( new ArgumentNullException("querableEmployee"));
            }

            _context.Employees.Remove(querableEmployee);
            _context.SaveChanges();

            return querableEmployee.EmployeeId;
        }

        public bool DidEmployeeUpdate(UpdateEmployeeDto employeeTo, Employee queryableEmployeeToUpdate)
        {
            queryableEmployeeToUpdate.EmployeeFirstName = employeeTo.EmployeeFirstName;
            queryableEmployeeToUpdate.EmployeeLastName = employeeTo.EmployeeLastName;
            queryableEmployeeToUpdate.EmployeeAddress = employeeTo.EmployeeAddress;
            queryableEmployeeToUpdate.EmployeeEmail = employeeTo.EmployeeEmail;

            _context.Employees.Update(queryableEmployeeToUpdate);
            _context.SaveChanges();

            // Romove Existing Roles Of Employee
            var queryableEmployeeRolesToDelete = _context.EmployeeRoles.Where(er => er.EmployeeId == employeeTo.EmployeeId);
            _context.EmployeeRoles.RemoveRange(queryableEmployeeRolesToDelete);
            _context.SaveChanges();

            // populating new roles for employee
            if (employeeTo.RoleIds != null)
            {
                EmployeeRole newEmployeeRoleAssignment = new EmployeeRole();

                foreach (var guid in employeeTo.RoleIds)
                {
                    newEmployeeRoleAssignment.Id = guid;     // roleId
                    newEmployeeRoleAssignment.EmployeeId = employeeTo.EmployeeId;

                    _context.EmployeeRoles.Add(newEmployeeRoleAssignment);
                    _context.SaveChanges();
                }

                return true;
            }

            return false;
        }
    }
}
