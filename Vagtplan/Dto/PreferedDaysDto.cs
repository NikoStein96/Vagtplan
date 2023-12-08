using Vagtplan.Models;

namespace Vagtplan.Dto
{
    public class PreferedDaysDto
    {
        public string FirebaseId { get; set; }
        public List<Weekday> Weekdays { get; set; }
    }
}
