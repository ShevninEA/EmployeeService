using EmployeeService.Models.Requests;
using EmployeeService.Models;
using EmployeeService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Services.Impl;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Работает с Department
    /// </summary>
    [Route("api")]
    [ApiController]
    public class DepartmentCotroller : ControllerBase
    {
        private readonly IDepartmentReposytory _departmentReposytory;
        public DepartmentCotroller(IDepartmentReposytory departmentReposytory)
        {
            _departmentReposytory = departmentReposytory;
        }

        [HttpPost("department/create")]
        public IActionResult CreateDepartmetn([FromBody] CreateDepartmentRequest request)
        {
            return Ok(_departmentReposytory.Create(new Models.Department
            {
                Id = request.Id,
                Description = request.Description
            }));
        }

        [HttpGet("department/getall")]
        public IActionResult GetAllDepartment()
        {
            return Ok(_departmentReposytory.GetAll());
        }

        [HttpGet("department/get/{id}")]
        public IActionResult GetByIdDepartment([FromRoute] Guid id)
        {
            return Ok(_departmentReposytory.GetById(id));
        }

        [HttpDelete("department/delete/{id}")]
        public IActionResult DeleteDepartment([FromRoute] Guid id)
        {
            _departmentReposytory.Delete(id);
            return Ok();
        }

        [HttpPut("department/update")]
        public IActionResult UpdateDepartment([FromBody] Department item)
        {
            _departmentReposytory.Update(item);
            return Ok();
        }
    }
}
