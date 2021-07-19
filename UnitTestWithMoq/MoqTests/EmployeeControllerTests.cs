using Autofac.Extras.Moq;
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
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;

namespace MoqTests
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void GetTest()
        {
            #region
            //using (var mock = AutoMock.GetLoose())
            //{
            //    mock.Mock<IGenericRepository<Employee>>()
            //        .Setup(x => x.GetAll())
            //        .Returns(GetSampleEmployee());
            //    var cls = mock.Create<EmployeeController>();
            //    var expected = GetSampleEmployee();

            //    var actual = cls.Get();

            //    Assert.True(actual != null);

            //}
            #endregion
            //arrange
            var service = new Mock<IGenericRepository<Employee>>();

            var employee = GetSampleEmployee();
            service.Setup(x => x.GetAll())
                .Returns(GetSampleEmployee);
            var controller = new EmployeeController(service.Object);
            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Employee>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleEmployee().Count(), actual.Count());
        }

        private List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
            {
                new Employee
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    PhoneNumber = "01682616787",
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
                    EmployeeId = 2
                },
                new Employee
                {
                    FirstName = "Jhon2",
                    LastName = "Doe2",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon2@gmail.com",
                    EmployeeId = 3
                }
            };
            return output;
        }
    }
}
