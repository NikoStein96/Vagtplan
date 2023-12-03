using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        // Properties to be stored in the database
        public DateTime StartTimeDb { get; set; }
        public DateTime EndTimeDb { get; set; }

        // Conversion between DateOnly and DateTime
        [NotMapped]
        public DateOnly StartTime
        {
            get => DateOnly.FromDateTime(StartTimeDb);
            set => StartTimeDb = value.ToDateTime(new TimeOnly());
        }

        [NotMapped]
        public DateOnly EndTime
        {
            get => DateOnly.FromDateTime(EndTimeDb);
            set => EndTimeDb = value.ToDateTime(new TimeOnly());
        }

        public ICollection<Day> Days { get; set; } = new List<Day>();
    }



    public class Day
    {
        public int Id { get; set; }

        // Database-compatible field
        public DateTime DayDateDb { get; set; }

        // Conversion between DateOnly and DateTime
        [NotMapped]
        public DateOnly DayDate
        {
            get => DateOnly.FromDateTime(DayDateDb);
            set => DayDateDb = value.ToDateTime(new TimeOnly());
        }

        public List<Shift> Shifts { get; set; }
        public int ScheduleId { get; set; }

        [JsonIgnore]
        public Schedule Schedule { get; set; } = null!;
    }


}
