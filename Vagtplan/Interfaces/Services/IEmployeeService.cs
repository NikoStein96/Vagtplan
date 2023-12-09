using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Interfaces.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees(string firebaseId);
        Task<bool> CreateEmployee(CreateEmployeeDto employee);
        bool CreateOwner(CreateOwnerDto employee);

        Employee GetEmployee(string id);

    }
}
