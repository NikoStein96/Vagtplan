using Microsoft.EntityFrameworkCore;
using Vagtplan.Interfaces;
using Vagtplan.Interfaces.Services;
using Vagtplan.Models;
using Vagtplan.Models.Dto;

namespace Vagtplan.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateShift(CreateShiftDto shift)
        {
            Shift newShift = new Shift();

            newShift.StartTime = shift.StartTime;
            newShift.EndTime = shift.EndTime;
            newShift.IsFisnished = shift.IsFisnished;
            newShift.DayId = shift.DayId;
            newShift.EmployeeId = shift.EmployeeId;
            newShift.Employee = _unitOfWork.Employees.GetEmployeeWithOrganisation(shift.EmployeeId);
            newShift.Day = _unitOfWork.Days.GetById(shift.DayId);
            newShift.Day.Shifts.Add(newShift);
            newShift.Employee.Shifts.Add(newShift);

            _unitOfWork.Shifts.Add(newShift);
            _unitOfWork.Complete();

            return true;
        }
    }
}
