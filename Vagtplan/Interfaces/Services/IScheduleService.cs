using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Interfaces.Services
{
    public interface IScheduleService
    {
        bool CreateSchedule(CreateScheduleDto schedule);

        Task<List<Schedule>> GetSchedules();

        Task<Schedule> GetSchedule(int id);

        void ExportSchedule(int id);

    }
}
