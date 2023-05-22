using AutoMapper;
using Employee_management_System_API.CustomActionFilters;
using Employee_management_System_API.Data;
using Employee_management_System_API.Models.Domain;
using Employee_management_System_API.Models.DTOs;
using Employee_management_System_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeManagementSystemDbContext dbContext;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeesController(EmployeeManagementSystemDbContext dbContext, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var employeesDomain = await employeeRepository.GetAllAsync();
            //Map Domain Models to DTO
            var employeeDto = mapper.Map<List<EmployeeDto>>(employeesDomain);
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) 
        {
            var employeeDomain = await employeeRepository.GetByIdAsync(id);
            if(employeeDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<EmployeeDto>(employeeDomain));
        
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddEmployeeRequestDto addEmployeeRequestDto)
        {
            //Map Dto to Domain model
            var employeeDomainModel = mapper.Map<Employee>(addEmployeeRequestDto);

          employeeDomainModel = await employeeRepository.CreateAsync(employeeDomainModel);

            //Map Domain Model back to to DTO
            var employeeDto = mapper.Map<EmployeeDto>(employeeDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = employeeDomainModel.Id }, employeeDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequestDto updateEmployeeRequestDto)
        {
            // Map Dto to Domain Model
            var employeeDomainModel = mapper.Map<Employee>(updateEmployeeRequestDto);
            // Check if the employee exists
           await employeeRepository.UpdateAsync(id, employeeDomainModel);
            if(employeeDomainModel == null)
            {
                return NotFound();
            }
            // convert domain model to DTO
            var employeeDto = mapper.Map<EmployeeDto>(employeeDomainModel);

            return Ok(employeeDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var EmployeeDomainModel = await employeeRepository.DeleteAsync(id);

            if(EmployeeDomainModel == null)
            {
                return NotFound();
            }

            var employeeDto = mapper.Map<EmployeeDto>(EmployeeDomainModel);
            return Ok(employeeDto);
        }
    }
}
