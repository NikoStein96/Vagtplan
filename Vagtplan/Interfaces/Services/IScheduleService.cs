using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Interfaces.Services
{
    public interface IScheduleService
    {
        bool CreateSchedule(CreateScheduleDto schedule);

        List<Schedule> GetSchedules(string id);

        Schedule GetSchedule(int id);

        void UpdateAvailableEmployees(UpdateAvailableDaysDto UpdateDays);

        void ExportSchedule(int id);

        void GenerateShiftsForSchedule(int id);

    }
}
