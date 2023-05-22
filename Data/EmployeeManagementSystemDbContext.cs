using Employee_management_System_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee_management_System_API.Data
{
    public class EmployeeManagementSystemDbContext:DbContext
    {
        public EmployeeManagementSystemDbContext(DbContextOptions<EmployeeManagementSystemDbContext> dbContextOptions): base(dbContextOptions)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
