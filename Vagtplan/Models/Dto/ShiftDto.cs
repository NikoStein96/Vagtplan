namespace Vagtplan.Models.Dto
{
    public class CreateShiftDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsFisnished { get; set; }
        public int DayId { get; set; }
        public string EmployeeId { get; set; }
    }
}
