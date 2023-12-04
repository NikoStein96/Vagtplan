using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public class Organisation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Employee? Owner { get; set; }
    }
}
