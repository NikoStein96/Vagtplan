using Vagtplan.Models;

namespace Vagtplan.Interfaces.Repositories
{
    public interface IScheduleRepository: IGenericRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedules();

        Schedule GetSchedule(int id);
    }
}
