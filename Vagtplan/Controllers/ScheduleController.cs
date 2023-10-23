using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Models;

namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly ShiftPlannerContext _context;

        public ScheduleController(ShiftPlannerContext dbContext)
        {

            _context = dbContext;

        }


        [HttpPost("createSchedule")]
        public ActionResult CreateSchedule(ScheduleDto scheduleDto) {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Validation: Check if StartTime is older than today
            if (scheduleDto.StartTime < today)
            {
                return BadRequest(new { error = "Start date cannot be older than today." });
            }

            // Validation: Check if StartTime is later than EndTime
            if (scheduleDto.StartTime > scheduleDto.EndTime)
            {
                return BadRequest(new { error = "Start date cannot be later than end date." });
            }

            Schedule schedule = new Schedule();
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            for (DateOnly date = scheduleDto.StartTime; date <= scheduleDto.EndTime; date = date.AddDays(1))
            {

                Day day = new Day();
                day.Schedule = schedule;
                day.DayDate = date;
                schedule.Days.Add(day);

            }

            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return Ok(schedule);
        }

        [HttpGet]
        public async Task<ActionResult<List<Schedule>>> GetSchedules()
        {

            var Schedules = await _context.Schedules.Include(schedule => schedule.Days).ToListAsync();
            return Schedules;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                                        .Include(schedule => schedule.Days)
                                        .FirstOrDefaultAsync(schedule => schedule.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }
    }
}
