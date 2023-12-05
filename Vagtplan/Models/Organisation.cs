using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public class Organisation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();


        [JsonIgnore]
        public Employee? Owner { get; set; }
    }
}
