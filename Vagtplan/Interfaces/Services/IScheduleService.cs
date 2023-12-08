using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Interfaces.Services
{
    public interface IScheduleService
    {
        bool CreateSchedule(CreateScheduleDto schedule);

        Task<List<Schedule>> GetSchedules();

        Schedule GetSchedule(int id);

        void UpdateAvailableEmployees(UpdateAvailableDaysDto UpdateDays);

        void ExportSchedule(int id);

    }
}
