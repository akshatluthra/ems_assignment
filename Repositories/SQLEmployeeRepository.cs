using Employee_management_System_API.Data;
using Employee_management_System_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee_management_System_API.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementSystemDbContext dbContext;

        public SQLEmployeeRepository(EmployeeManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
           await dbContext.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteAsync(Guid id)
        {
            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existingEmployee == null) 
            {
                return null;
            }

            dbContext.Remove(existingEmployee);
            await dbContext.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
          return  await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
           return await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee?> UpdateAsync(Guid id, Employee employee)
        {
            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(existingEmployee == null)
            {
                return null;
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.officeLocation = employee.officeLocation;
            existingEmployee.profileImageUrl = employee.profileImageUrl;
            await dbContext.SaveChangesAsync();
            return existingEmployee;
        }
    }
}
