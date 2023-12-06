using Vagtplan.Models.Dto;

namespace Vagtplan.Dto
{
    public class OrganisationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmployeeDTO>? Employees { get; set; }
    }
}