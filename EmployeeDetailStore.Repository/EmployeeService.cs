using EmployeeDetailStore.Model.DatabaseModels;
using EmployeeDetailStore.Repository.Interfaces;
using EmployeeDetailStore.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDetailStore.Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException($"Invalid employee ID-{id}");
                }

                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    throw new NotFoundException("Employee not found.");
                }

                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(GetEmployeeByIdAsync));
                throw;
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(GetAllEmployeesAsync));
                throw;
            }
        }

        public async Task<List<Employee>> GetEmployeeBySearchValueAsync(string searchValue)
        {
            try
            {
                List<Employee> employees;
                if (string.IsNullOrEmpty(searchValue))
                {
                    employees = await _employeeRepository.GetAllAsync();
                    return employees;
                }
                searchValue = searchValue.ToLower();
                employees = await _employeeRepository.FindAsync(e => e.FirstName.ToLower().Contains(searchValue) || e.LastName.ToLower().Contains(searchValue));
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(GetEmployeeBySearchValueAsync));
                throw;
            }
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }

                _employeeRepository.Add(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(CreateEmployeeAsync));
                throw; // Re-throw the exception for the caller to handle.
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee == null || employee.EmployeeId <= 0)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                var result = await _employeeRepository.FindAsync(x => x.EmployeeId == employee.EmployeeId);
                if (result == null || result.Count==0)
                {
                    throw new NotFoundException("Employee not found.");
                }
                _employeeRepository.Update(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(UpdateEmployeeAsync));
                throw; // Re-throw the exception for the caller to handle.
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException($"Invalid employee ID-{id}");
                }

                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    throw new NotFoundException("Employee not found.");
                }

                _employeeRepository.Remove(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method:{method}", nameof(DeleteEmployeeAsync));
                throw; // Re-throw the exception for the caller to handle.
            }
        }
    }
}
