using EmployeeService.Models;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using EmployeeService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Работает с Employee
    /// </summary>
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeReposytory _employeeReposytory;
        public EmployeeController(IEmployeeReposytory employeeReposytory)
        {
            _employeeReposytory = employeeReposytory;
        }

        [HttpPost("employee/create")]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            return Ok(_employeeReposytory.Create(new Models.Employee 
            {
                Id = request.Id,
                DepartmentId = request.DepartmentId,
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Salary = request.Salary
            }));
        }

        [HttpGet("employee/getall")]
        public IActionResult GetAllEmployee()
        {
            return Ok(_employeeReposytory.GetAll());
        }

        [HttpGet("employee/get/{id}")]
        public IActionResult GetByIdEmployee([FromRoute] int id)
        {
            return Ok(_employeeReposytory.GetById(id));
        }

        [HttpDelete("employee/delete/{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            _employeeReposytory.Delete(id);
            return Ok();
        }

        [HttpPut("employee/update")]
        public IActionResult UpdateEmployee([FromBody] Employee item)
        {
            _employeeReposytory.Update(item);
            return Ok();
        }
    }
}
