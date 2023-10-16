namespace Vagtplan.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
        public int Pay { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}
