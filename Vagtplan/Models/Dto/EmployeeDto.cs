namespace Vagtplan.Models.Dto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateOwnerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string OrganisationName { get; set; }
    }
}
