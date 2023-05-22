using Employee_management_System_API.Models.Domain;

namespace Employee_management_System_API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(Guid id);

        Task<Employee> CreateAsync(Employee employee);

        Task<Employee?> UpdateAsync(Guid id, Employee employee);

        Task<Employee?> DeleteAsync(Guid id);
    }
}
