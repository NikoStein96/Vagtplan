using Vagtplan.Interfaces.Repositories;

namespace Vagtplan.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IScheduleRepository Schedules { get; }
        IShiftRepository Shifts { get; }

        IOrganisationRepository Organisations { get; }

        IDayRepository Days { get; }
        int Complete();
    }
}
