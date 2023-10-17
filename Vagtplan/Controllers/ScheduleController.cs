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

            Schedule schedule = new Schedule();
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            for (DateTime date = scheduleDto.StartTime; date <= scheduleDto.EndTime; date = date.AddDays(1))
            {

                Day day = new Day();
                day.Schedule = schedule;
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





    }
}
