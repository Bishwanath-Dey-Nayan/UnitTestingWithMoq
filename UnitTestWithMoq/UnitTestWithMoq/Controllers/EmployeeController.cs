using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitTestWithMoq.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestWithMoq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IGenericRepository<Employee> repo = null;

        public EmployeeController(IGenericRepository<Employee> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            var model = repo.GetAll();
            return Ok(model);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public  ActionResult<Employee> GetEmployeeById(long id)
        {
            Employee employee = repo.GetById(id);
            if(employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(employee);
        }

        [HttpPost("CreateEmployee")]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null");
            }
            repo.Insert(employee);
            return CreatedAtRoute("GetEmployeeById", new { Id = employee.EmployeeId}, employee);
        }

        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null");
            }
            repo.Update(employee);
            return NoContent();
        }

        public bool checkIfUserCanBeVoter(int age)
        {
            return (age >= 18) ? true : false; 
        }
    }
}
