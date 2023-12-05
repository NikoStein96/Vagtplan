using FirebaseAdmin.Auth;
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

            var employees = await _context.Employees.Include(employee => employee.Shifts).Include(employee => employee.Organisation).ToListAsync();
            return Ok(employees);
        }

        [HttpPost("Employees")]
        public async Task<IActionResult> PostEmployeesAsync(CreateEmployeeDto employee) {

            var UserId = HttpContext.Items["FirebaseUserId"] as string;

            if (UserId == null)
            {
                return Unauthorized();
            }
            UserRecordArgs urg = new UserRecordArgs();
            urg.Email = employee.Email;
            urg.Password = "simonertyk";

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(urg);
            Employee newEmployee = new Employee();
            newEmployee.Name = employee.Name;
            newEmployee.Email = employee.Email;
            newEmployee.FirebaseId = userRecord.Uid;
            newEmployee.Organisation = _context.Organisations.Where(organisation => organisation.Owner.FirebaseId == UserId).First();
            newEmployee.Organisation.Employees.Add(newEmployee);

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return Ok(employee);
        }


        [HttpPost("Owners")]
        public IActionResult CreateOwner(CreateOwnerDto owner)
        {
            var UserId = HttpContext.Items["FirebaseUserId"] as string;

            if (UserId == null)
            {
                return Unauthorized();
            }

            Employee newOwner = new Employee();
            newOwner.FirebaseId = UserId;
            newOwner.Name = owner.Name;
            newOwner.Email = owner.Email;
            newOwner.Role = UserRole.Owner;

            Organisation org = new Organisation();
            org.Name = owner.OrganisationName;
            org.Owner = newOwner;

            newOwner.Organisation = org;

            _context.Employees.Add(newOwner);
            _context.Organisations.Add(org);
            _context.SaveChanges();
            return Ok(owner);
        }
    }
}
