using System.ComponentModel.DataAnnotations;

namespace Employee_management_System_API.Models.DTOs
{
    public class AddEmployeeRequestDto
    {
        [Required]
        [MinLength(10, ErrorMessage = "Name has to be minimum of 10 characters")]
        [MaxLength (25, ErrorMessage = "Name has to be maximum of 25 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength (5, ErrorMessage = "Department name has to be minimum of 5 characters")]
        [MaxLength (25, ErrorMessage = "Department name has to be maximum of 25 characters")]
        public string Department { get; set; }

        [Required]
        public string officeLocation { get; set; }
        
        public string? profileImageUrl { get; set; }
    }
}
