using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ShiftPlannerContext _context;
        
        public EmployeeController(ShiftPlannerContext dbContext) {
        
            _context = dbContext;

        }


        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {

            var employees = await _context.Employees.Include(employee => employee.Shifts).ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult PostEmployees(CreateEmployeeDto employee) {

            Employee newEmployee = new Employee();
            newEmployee.FirebaseId = employee.FirebaseId;
            newEmployee.Name = employee.Name;
            newEmployee.Email = employee.Email;
            newEmployee.Phone = employee.Phone;
            newEmployee.City = employee.City;
            newEmployee.JobTitle = employee.JobTitle;
            newEmployee.Age = employee.Age;
            newEmployee.Pay = employee.Pay;
            
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return Ok(employee);
        }
    }
}
