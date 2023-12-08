using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vagtplan.Models
{
    public enum UserRole
    {
        Owner,
        Employee
    }

    public enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class PreferedWorkDay
    {
        public string EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
        public Weekday Weekday { get; set; }
    }


    public class Employee
    {
        [Key]
        public string FirebaseId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public int Age { get; set; }
        public int Pay { get; set; }
        public UserRole Role { get; set; } = UserRole.Employee;
        public List<PreferedWorkDay> PreferedWorkDays { get; set; } = new List<PreferedWorkDay>();
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = null!;
        public List<Shift> Shifts { get; set; } = new List<Shift>();

    }
}
