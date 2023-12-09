using Google.Api.Gax;
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


        public Schedule GetSchedule(int id)
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

        public async void UpdateAvailableEmployees(UpdateAvailableDaysDto UpdateDays) {

                var schedule = _unitOfWork.Schedules.GetSchedule(UpdateDays.ScheduleId);
                var employee = _unitOfWork.Employees.GetEmployee(UpdateDays.FirebaseId);
                if (schedule == null)
                {
                    throw new Exception("Not Found");
                }

                foreach (Day day in schedule.Days)
                {
                    if (UpdateDays.Days != null)
                    {
                        if (UpdateDays.Days.Contains(day.DayDate))
                        {
                            day.AvailableEmployees.Add(employee);
                        }
                    }
                }
            _unitOfWork.Complete();
        }

        public void GenerateShiftsForSchedule(int id)
        {
            var schedule = _unitOfWork.Schedules.GetSchedule(id);
            var allEmployees = _unitOfWork.Employees.GetEmployees();

            // Track the number of shifts assigned to each employee
            var employeeShiftCount = new Dictionary<string, int>();
            foreach (var emp in allEmployees)
            {
                employeeShiftCount[emp.FirebaseId] = 0;
            }

            foreach (var day in schedule.Days)
            {
                int shiftsAssigned = 0;

                // Prioritize employees with preferred days and fewer shifts
                var preferredEmployees = day.AvailableEmployees
                    .OrderBy(emp => employeeShiftCount[emp.FirebaseId])
                    .ToList();

                foreach (var employee in preferredEmployees)
                {
                    if (shiftsAssigned < day.ShiftsNeeded)
                    {
                        day.Shifts.Add(new Shift { Employee = employee, Day = day });
                        shiftsAssigned++;
                        employeeShiftCount[employee.FirebaseId]++;
                    }
                }

                // Assign remaining shifts to other available employees with fewer shifts
                if (shiftsAssigned < day.ShiftsNeeded)
                {
                    var otherAvailableEmployees = allEmployees
                        .Except(preferredEmployees)
                        .OrderBy(emp => employeeShiftCount[emp.FirebaseId])
                        .ToList();

                    foreach (var employee in otherAvailableEmployees)
                    {
                        if (shiftsAssigned < day.ShiftsNeeded)
                        {
                            day.Shifts.Add(new Shift { Employee = employee, Day = day });
                            shiftsAssigned++;
                            employeeShiftCount[employee.FirebaseId]++;
                        }
                    }
                }

                _unitOfWork.Complete();
            }
        
    }
    }
}
