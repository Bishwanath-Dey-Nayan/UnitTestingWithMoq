using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestWithMoq;
using UnitTestWithMoq.Repository;
using Xunit;

namespace MoqTests
{
    public class GenericRepositoryTest
    {
        private DbContextOptions<ApplicationContext> options;
        public GenericRepositoryTest()
        {
            options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDatabase")
            .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Employees.Add(new Employee
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon@gmail.com",
                    EmployeeId = 1
                });
                context.Employees.Add(new Employee
                {
                    FirstName = "Jhon1",
                    LastName = "Doe1",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon@gmail.com",
                    EmployeeId = 4
                });
                context.Employees.Add(new Employee
                {
                    FirstName = "Jhon2",
                    LastName = "Doe2",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon2@gmail.com",
                    EmployeeId = 5
                });
                context.SaveChanges();
            }
            
        }

        [Fact]
        public void GetAllTest()
        {
            using(var context = new ApplicationContext(options))
            {
                var repo = new GenericRepository<Employee>(context);
                var employees = repo.GetAll();
                Assert.Equal(3, employees.Count());
            }

        }

        [Fact]
        public void GetByIdTest()
        {
            using (var context = new ApplicationContext(options))
            {
                var repo = new GenericRepository<Employee>(context);
                Employee employe = repo.GetById((long)1);
                Assert.Equal("Jhon", employe.FirstName);
                Assert.Equal("jhon@gmail.com", employe.Email);
            }
        }
    }
}
