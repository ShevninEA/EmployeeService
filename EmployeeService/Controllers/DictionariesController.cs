using EmployeeService.Models;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using EmployeeService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Работает с EmployeeType
    /// </summary>
    [Route("api")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IEmployeeTypeReposytory _employeeTypeReposytory;
        public DictionariesController(IEmployeeTypeReposytory employeeTypeReposytory)
        {
            _employeeTypeReposytory = employeeTypeReposytory;
        }

        [HttpPost("employee-types/create")]
        public IActionResult CreateEmployeeTypes([FromBody] CreateEmployeeTypeRequest request)
        {
            return Ok(_employeeTypeReposytory.Create(new Models.EmployeeType 
            {
                Id = request.Id,
                Description = request.Description
            }));
        }

        [HttpGet("employee-types/getall")]
        public IActionResult GetAllEmployeeTypes()
        {
            return Ok(_employeeTypeReposytory.GetAll());
        }

        [HttpGet("employee-types/get/{id}")]
        public IActionResult GetByIdEmployeeTypes([FromRoute] int id)
        {
            return Ok(_employeeTypeReposytory.GetById(id));
        }

        [HttpDelete("employee-types/delete/{id}")]
        public IActionResult DeleteEmployeeTypes([FromRoute] int id)
        {
            _employeeTypeReposytory.Delete(id);
            return Ok();
        }

        [HttpPut("employee-types/update")]
        public IActionResult UpdateEmployeeTypes([FromBody] EmployeeType item)
        {
            _employeeTypeReposytory.Update(item);
            return Ok();
        }
    }
}
