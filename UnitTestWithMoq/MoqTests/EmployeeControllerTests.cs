using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using UnitTestWithMoq;
using UnitTestWithMoq.Controllers;
using UnitTestWithMoq.Repository;
using Xunit;

namespace MoqTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IGenericRepository<Employee>> service;
        public EmployeeControllerTests()
        {
            service = new Mock<IGenericRepository<Employee>>();
        }
        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void GetEmployee_ListOfEmployee_EmployeeExistsInRepo()
        {
            //arrange
            var employee = GetSampleEmployee();
            service.Setup(x => x.GetAll())
                .Returns(GetSampleEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployee();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Employee>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleEmployee().Count(), actual.Count());
        }

        [Fact]
        public void GetEmployeeById_EmployeeObject_EmployeewithSpecificeIdExists()
        {
            //arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            service.Setup(x => x.GetById((long)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployeeById((long)1);
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo (firstEmployee);
        }

        [Fact]
        public void GetEmployeeById_shouldReturnBadRequest_EmployeeWithIDNotExists()
        {
            //arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            service.Setup(x => x.GetById((long)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployeeById((long)2);

            //assert
            var result = actionResult.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(20)]
        public void checkIfUserCanBeVoter_true_ageGreaterThan18(int age)
        {
            //arrange
            var controller = new EmployeeController(null);

            //act
            var actual = controller.checkIfUserCanBeVoter(age);

            //Assert
            Assert.True(actual);

        }

        [Theory]
        [InlineData(17)]
        [InlineData(15)]
        public void checkIfUserCanBeVoter_true_ageLessThan18(int age)
        {
            //arrange
            var controller = new EmployeeController(null);

            //act
            var actual = controller.checkIfUserCanBeVoter(age);

            //Assert
            Assert.False(actual);

        }

        [Fact]
        public void CreateEmployee_CreatedStatus_PassingEmployeeObjectToCreate()
        {
            var employees = GetSampleEmployee();
            var newEmployee = employees[0];
            var controller = new EmployeeController(service.Object);
            var actionResult = controller.CreateEmployee(newEmployee);
            var result = actionResult.Result;
            Assert.IsType<CreatedAtRouteResult>(result); 
                
        }

        private List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
            {
                new Employee
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    PhoneNumber = "01682616789",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon@gmail.com",
                    EmployeeId = 1
                },
                new Employee
                {
                    FirstName = "Jhon1",
                    LastName = "Doe1",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon@gmail.com",
                    EmployeeId = 4
                },
                new Employee
                {
                    FirstName = "Jhon2",
                    LastName = "Doe2",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon2@gmail.com",
                    EmployeeId = 5
                }
            };
            return output;
        }
    }
}
