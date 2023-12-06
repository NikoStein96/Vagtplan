using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Interfaces.Services;
using Vagtplan.Models;
using Vagtplan.Models.Dto;


namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) {

            _employeeService = employeeService;
        }


        [HttpGet]
        public  ActionResult<List<Employee>> GetEmployees()
        {
            var employees =  _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpPost("Employees")]
        public async Task<IActionResult> PostEmployeesAsync(CreateEmployeeDto employee) {

            var UserId = HttpContext.Items["FirebaseUserId"] as string;

            employee.FirebaseId = UserId;

            await _employeeService.CreateEmployee(employee);
            
            return Ok();
        }


        [HttpPost("Owners")]
        public IActionResult CreateOwner(CreateOwnerDto owner)
        {
            var UserId = HttpContext.Items["FirebaseUserId"] as string;

            owner.FirebaseId = UserId;

            _employeeService.CreateOwner(owner);
            
            return Ok();
        }
    }
}
