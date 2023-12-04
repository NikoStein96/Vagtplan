using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Vagtplan.Models
{
    public enum UserRole
    {
        Owner,
        Employee
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
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = null!;
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
