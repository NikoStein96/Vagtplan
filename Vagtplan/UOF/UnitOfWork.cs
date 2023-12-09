using Google;
using Vagtplan.Data;
using Vagtplan.Interfaces;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Repositories;

namespace Vagtplan.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShiftPlannerContext _context;
        public UnitOfWork(ShiftPlannerContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Schedules = new ScheduleRepository(_context);
            Shifts = new ShiftRepository(_context);
            Days = new DayRepository(_context);
            Organisations = new OrganisationRepository(_context);
        }
        public IEmployeeRepository Employees { get; private set; }
        public IScheduleRepository Schedules { get; private set; }
        public IShiftRepository Shifts { get; private set; }

        public IOrganisationRepository Organisations { get; private set; }

        public IDayRepository Days { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
