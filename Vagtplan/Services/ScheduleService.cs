using Microsoft.EntityFrameworkCore;
using Vagtplan.Interfaces;
using Vagtplan.Interfaces.Services;
using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleService(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }

        public bool CreateSchedule(CreateScheduleDto scheduleDto)
        {
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

            _unitOfWork.Schedules.Add(schedule);
            _unitOfWork.Complete();

            return true;
        }


        public Task<Schedule> GetSchedule(int id)
        {
            return _unitOfWork.Schedules.GetSchedule(id);
        }

        public Task<List<Schedule>> GetSchedules()
        {
            return _unitOfWork.Schedules.GetSchedules();
        }

        public void ExportSchedule(int id)
        {
            throw new NotImplementedException();
        }
    }
}
