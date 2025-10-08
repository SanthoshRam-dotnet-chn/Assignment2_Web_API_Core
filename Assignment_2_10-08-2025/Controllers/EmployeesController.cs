using Assignment_2_10_08_2025.Models;
using Assignment_2_10_08_2025.Repo;
using Assignment_2_10_08_2025.Models;
using Assignment_2_10_08_2025.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById([FromRoute] int id)
        {
            var employee = _repo.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("bydept")]
        public ActionResult<List<Employee>> GetEmployeesByDept([FromQuery] string department)
        {
            return Ok(_repo.GetByDepartment(department));
        }

        [HttpPost]
        public ActionResult AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repo.Exists(employee.Id))
                return Conflict("Employee with this ID already exists.");

            _repo.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee updatedEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _repo.GetById(id);
            if (employee == null) return NotFound();

            updatedEmployee.Id = id;
            _repo.Update(updatedEmployee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee([FromRoute] int id)
        {
            var employee = _repo.GetById(id);
            if (employee == null) return NotFound();

            _repo.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}/email")]
        public ActionResult UpdateEmployeeEmail([FromRoute] int id, [FromBody] string email)
        {
            var employee = _repo.GetById(id);
            if (employee == null) return NotFound();

            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
                return BadRequest("Invalid email format.");

            _repo.UpdateEmail(id, email);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS");
            return Ok();
        }
    }
}

