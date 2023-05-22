using AutoMapper;
using Employee_management_System_API.Models.Domain;
using Employee_management_System_API.Models.DTOs;

namespace Employee_management_System_API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<AddEmployeeRequestDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeRequestDto, Employee>().ReverseMap();
            
        }
    }
}
