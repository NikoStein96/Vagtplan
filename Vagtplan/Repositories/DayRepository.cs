using Vagtplan.Data;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Models;

namespace Vagtplan.Repositories
{
    public class DayRepository : GenericRepository<Day>, IDayRepository
    {
        public DayRepository(ShiftPlannerContext context) : base(context)
        {
        }
    }
}
