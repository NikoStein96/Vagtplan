namespace Vagtplan.Models.Dto
{
    public class ScheduleDto
    {
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }

    }

    public class CreateScheduleDto
    {
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }

    }
}
