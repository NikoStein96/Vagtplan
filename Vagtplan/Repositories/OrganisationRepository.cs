using Vagtplan.Data;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Models;

namespace Vagtplan.Repositories
{
    public class OrganisationRepository : GenericRepository<Organisation>, IOrganisationRepository
    {
        public OrganisationRepository(ShiftPlannerContext context) : base(context)
        {
        }
    }
}
