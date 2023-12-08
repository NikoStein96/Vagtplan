using Google.Api.Gax;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Models;

namespace Vagtplan.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ShiftPlannerContext context) : base(context)
        { }

        public async Task<List<Schedule>> GetSchedules()
        {
            return await _context.Schedules.Include(schedule => schedule.Days).ThenInclude(days => days.Shifts).ToListAsync();
        }


        public async Task<Schedule> GetSchedule(int id)
        {
            return await _context.Schedules
                                        .Include(schedule => schedule.Days).ThenInclude(day => day.Shifts)
                                        .ThenInclude(shift => shift.Employee)
                                        .Include(schedule => schedule.Days).ThenInclude(availableEmployees => availableEmployees.AvailableEmployees)
                                        .FirstOrDefaultAsync(schedule => schedule.Id == id);
        }
    }
}
