using EmployeeDetailStore.Model.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDetailStore.Repository.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeeBySearchValueAsync(string searchValue);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);

    }
}
