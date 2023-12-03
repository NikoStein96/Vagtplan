namespace Vagtplan.Models.Dto
{
    public class CreateEmployeeDto
    {
        public string FirebaseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
        public int Pay { get; set; }
    }
}
