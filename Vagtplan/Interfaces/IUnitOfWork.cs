using Vagtplan.Interfaces.Repositories;

namespace Vagtplan.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        int Complete();
    }
}
