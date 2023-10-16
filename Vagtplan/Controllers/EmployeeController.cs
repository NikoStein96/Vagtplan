using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vagtplan.Data;
using Vagtplan.Models;

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
        public IActionResult GetEmployees() {
        
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }


        [HttpPost]
        public IActionResult PostEmployees(Employee employee) {
        
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }







    }
}
