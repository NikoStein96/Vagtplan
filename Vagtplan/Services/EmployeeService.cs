using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Interfaces;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Interfaces.Services;

using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Employee> GetEmployees(string firebaseId)
        {
            var employee = _unitOfWork.Employees.Find(e => e.FirebaseId == firebaseId).First();

            var employees = _unitOfWork.Employees.Find(e => e.OrganisationId == employee.OrganisationId).ToList();

            return employees;
        }

        public Employee GetEmployee(string employeeId) {
            return _unitOfWork.Employees.GetEmployee(employeeId);
        }

        public async Task<bool> CreateEmployee(CreateEmployeeDto employee)
        {
            UserRecordArgs urg = new UserRecordArgs();
            urg.Email = employee.Email;
            urg.Password = "simonertyk";

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(urg);
            Employee newEmployee = new Employee();
            newEmployee.Name = employee.Name;
            newEmployee.Email = employee.Email;
            newEmployee.FirebaseId = userRecord.Uid;
            newEmployee.Organisation = _unitOfWork.Employees.GetEmployeeWithOrganisation(employee.FirebaseId).Organisation;
            newEmployee.Organisation.Employees.Add(newEmployee);

;
            _unitOfWork.Employees.Add(newEmployee);
            _unitOfWork.Complete();
            return true;

        }

        public bool CreateOwner(CreateOwnerDto employee)
        {
            var newOwner = new Employee
            {
                FirebaseId = employee.FirebaseId,
                Name = employee.Name,
                Email = employee.Email,
                Role = UserRole.Owner
            };

            var org = new Organisation
            {
                Name = employee.OrganisationName,
            };
            org.Employees.Add(newOwner);
            newOwner.Organisation = org;
            _unitOfWork.Employees.Add(newOwner);
            _unitOfWork.Complete();
;
            return true;
        }

    }
}
