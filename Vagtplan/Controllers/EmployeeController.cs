using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Dto;
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

        [HttpGet("EmployeeProfile")]
        public Employee GetEmployeeProfile()
        {
            var UserId = HttpContext.Items["FirebaseUserId"] as string;

            return _employeeService.GetEmployee(UserId);
        }


        [HttpGet]
        public  ActionResult<List<Employee>> GetEmployees()
        {
            var UserId = HttpContext.Items["FirebaseUserId"] as string;
            var employees =  _employeeService.GetEmployees(UserId);
            return Ok(employees);
        }

        [HttpPost("Employees")]
        public async Task<IActionResult> PostEmployeesAsync(CreateEmployeeDto employee) {

            await _employeeService.CreateEmployee(employee);
            
            return Ok();
        }


        [HttpPost("Owners")]
        public IActionResult CreateOwner(CreateOwnerDto owner)
        {
            _employeeService.CreateOwner(owner);
            
            return Ok();
        }

      
    }
}
