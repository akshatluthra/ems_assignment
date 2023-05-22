namespace Employee_management_System_API.Models.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string officeLocation { get; set; }

        public string? profileImageUrl { get; set; }
    }
}
