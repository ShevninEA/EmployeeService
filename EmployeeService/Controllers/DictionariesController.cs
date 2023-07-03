using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using EmployeeService.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Работает с EmployeeType
    /// </summary>
    [Authorize]
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
        public ActionResult<int> CreateEmployeeType([FromQuery] string description)
        {
            return Ok(_employeeTypeReposytory.Create(new EmployeeType
            {
                Description = description
            }));
        }

        [HttpGet("employee-types/getall")]
        public ActionResult<IList<CreateEmployeeTypeRequests>> GetAllEmployeeTypes()
        {
            return Ok(_employeeTypeReposytory.GetAll().Select(et =>
                new CreateEmployeeTypeRequests
                {
                    Id = et.Id,
                    Description = et.Description
                }
                ).ToList());
        }

        [HttpGet("employee-types/get-id")]
        public IActionResult GetByIdEmployeeTypes([FromQuery] int id)
        {
            return Ok(_employeeTypeReposytory.GetById(id));
        }

        [HttpDelete("employee-types/delete")]
        public ActionResult<bool> DeleteEmployeeType([FromQuery] int id)
        {
            return Ok(_employeeTypeReposytory.Delete(id));
        }

        [HttpPut("employee-types/update")]
        public ActionResult<bool> UpdateEmployeeTypes([FromQuery] EmployeeType item)
        {
            _employeeTypeReposytory.Update(item);
            return Ok();
        }
    }
}
