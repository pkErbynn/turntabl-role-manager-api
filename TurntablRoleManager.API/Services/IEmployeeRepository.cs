using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurntablRoleManager.API.Entities;
using TurntablRoleManager.API.Models;

namespace TurntablRoleManager.API.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<DetailEmployeeDTO> GetEmployees();
        DetailEmployeeDTO GetEmployee(int id);
        EmployeeTo AssignEmployeeWithRoles(AddEmployeeDTO employeeDTO);
        int DeleteEmployee(int id);
        bool DidEmployeeUpdate(UpdateEmployeeDto employeeTo, Employee queryableEmployeeToUpdate);
    }
}
