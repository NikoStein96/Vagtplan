using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public class Shift
    {
        public int Id {get; set;}
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set;}

        public int DayId { get; set;}

        [JsonIgnore]
        public Day Day { get; set; } = null!;

        public bool IsFisnished { get; set;}

        public string EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; } = null!;
    }
}
