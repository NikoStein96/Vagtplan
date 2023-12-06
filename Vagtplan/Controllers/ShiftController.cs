using Microsoft.AspNetCore.Mvc;
using Vagtplan.Data;
using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftPlannerContext _context;

        public ShiftController(ShiftPlannerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift(CreateShiftDto shift)
        {

            Shift newShift = new Shift();

            newShift.StartTime = shift.StartTime;
            newShift.EndTime = shift.EndTime;
            newShift.IsFisnished = shift.IsFisnished;
            newShift.DayId = shift.DayId;
            newShift.EmployeeId = shift.EmployeeId;
            newShift.Employee = _context.Employees.Where(e => e.FirebaseId == shift.EmployeeId).SingleOrDefault();
            Console.WriteLine(newShift.Employee.Name);
            newShift.Day =  _context.Days.Where(d => d.Id == shift.DayId).SingleOrDefault();
            newShift.Day.Shifts.Add(newShift);
            newShift.Employee.Shifts.Add(newShift);

            _context.Shifts.Add(newShift);
            await _context.SaveChangesAsync();

            return Ok(newShift);
        }


    }
}
