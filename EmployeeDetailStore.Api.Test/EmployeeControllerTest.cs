using System;
using Xunit;
using NSubstitute;
using EmployeeDetailStore.Api.Controllers;
using Microsoft.Extensions.Logging;
using EmployeeDetailStore.Repository.Interfaces;
using System.Collections.Generic;
using DatabaseModel = EmployeeDetailStore.Model.DatabaseModels;
using DtoModel = EmployeeDetailStore.Model;
using Microsoft.AspNetCore.Mvc;
using EmployeeDetailStore.Utilities.Mapper;
using EmployeeDetailStore.Utilities;
using System.Threading.Tasks;

namespace EmployeeDetailStore.Api.Test
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void Get_ShouldReturnOkAndAllEmployee_IfExists()
        {
            // Arrange
            var logger = Substitute.For<ILogger<EmployeeController>>();
            var service = Substitute.For<IEmployeeService>();
            var employee = new List<DatabaseModel.Employee>()
            {
                new DatabaseModel.Employee(),
                new DatabaseModel.Employee()
            };
            var controller = new EmployeeController(logger, service);
            service.GetAllEmployeesAsync().Returns(employee);

            // Act
            var result = controller.Get().Result;

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<List<DtoModel.Employee>>(okResult.Value);
            Assert.Equal(employee.Count, actualResult.Count);
        }

        [Fact]
        public void Get_ShouldReturnNotFound_WhenNotFoundException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<EmployeeController>>();
            var service = Substitute.For<IEmployeeService>();
            var employee = new List<DatabaseModel.Employee>()
            {
                new DatabaseModel.Employee(),
                new DatabaseModel.Employee()
            };
            var controller = new EmployeeController(logger, service);
            service.GetAllEmployeesAsync().Returns(Task.FromException<List<DatabaseModel.Employee>>(new NotFoundException("Not found")));

            // Act
            var result = controller.Get().Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldReturn500Error_WhenException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<EmployeeController>>();
            var service = Substitute.For<IEmployeeService>();
            var employee = new List<DatabaseModel.Employee>()
            {
                new DatabaseModel.Employee(),
                new DatabaseModel.Employee()
            };
            var controller = new EmployeeController(logger, service);
            service.GetAllEmployeesAsync().Returns(Task.FromException<List<DatabaseModel.Employee>>(new Exception()));

            // Act
            var result = controller.Get().Result;

            // Assert
            var expectedResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, expectedResult.StatusCode);
        }

    }
}
