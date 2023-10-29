using EmployeeDetailStore.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using NSubstitute;
using EmployeeDetailStore.Model.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDetailStore.Repository.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturEmployee_WhenNoExceptionInGetAllEmployeesAsync()
        {
            // Arrange
            var logger = Substitute.For<ILogger<EmployeeService>>();
            var repository = Substitute.For<IRepository<Employee>>();
            var expectedValue = new List<Employee>()
            {
                new Employee(){ FirstName = "Sunil", EmployeeId =1},
                new Employee(){ FirstName = "John", EmployeeId =2}
            };
            repository.GetAllAsync().Returns(expectedValue);
            var employeeService = new EmployeeService(logger, repository);

            // Act
            var result = employeeService.GetAllEmployeesAsync().Result;

            // Assert
            Assert.Equal(expectedValue.Count, result.Count);
            Assert.Equal(expectedValue[0].EmployeeId, result[0].EmployeeId);
            Assert.Equal(expectedValue[0].FirstName, result[0].FirstName);
        }

        [Fact]
        public void ShouldThrowException_WhenExceptionInGetAllEmployeesAsync()
        {
            // Arrange
            var logger = Substitute.For<ILogger<EmployeeService>>();
            var repository = Substitute.For<IRepository<Employee>>();
            var expectedValue = new List<Employee>()
            {
                new Employee(){ FirstName = "Sunil", EmployeeId =1},
                new Employee(){ FirstName = "John", EmployeeId =2}
            };
            repository.GetAllAsync().Returns(Task.FromException<List<Employee>>(new Exception()));
            var employeeService = new EmployeeService(logger, repository);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(()=> employeeService.GetAllEmployeesAsync());
        }
    }
}
