using Vagtplan.Dto;

namespace Vagtplan.Models.Dto
{
    public class EmployeeDTO
    {
        public string FirebaseId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public int Age { get; set; }
        public int Pay { get; set; }
        public UserRole Role { get; set; }
        public OrganisationDTO Organisation { get; set; }
    }

    public class UpdateAvailableDaysDto {
        public List<DateOnly> Days { get; set; }
        public string FirebaseId { get; set; }
        public int ScheduleId { get; set; }
    
    }

    public class CreateEmployeeDto
    {
        public string? FirebaseId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateOwnerDto
    {
        public string? FirebaseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OrganisationName { get; set; }
    }
}
