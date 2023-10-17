using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public class Schedule
    {

        public int Id { get; set; }
        public DateTime  StartTime {  get; set; } 
        public DateTime EndTime { get; set; }


        public ICollection<Day> Days { get; set; } = new List<Day>();

    }



    public class Day
    {
        public int Id { get; set; }
        public List<Shift> Shifts { get; set;}
        public int ScheduleId { get; set; }

        [JsonIgnore]
        public Schedule Schedule { get; set; } = null!;

    }
  

}
