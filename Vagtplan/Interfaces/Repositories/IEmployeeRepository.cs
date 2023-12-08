using Vagtplan.Models;

namespace Vagtplan.Interfaces.Repositories
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        Employee GetEmployeeWithOrganisation(string firebaseId);

        List<Employee> GetEmployees();

        void SetPreferedWorkDays(string firebaseId, List<PreferedWorkDay> workDays);
    }
}
