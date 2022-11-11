using EmployeeService.Models.Requests;
using EmployeeService.Models;
using EmployeeService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Services.Impl;
using EmployeeService.Data;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Работает с Department
    /// </summary>
    [Route("api")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentReposytory _departmentReposytory;
        public DepartmentController(IDepartmentReposytory departmentReposytory)
        {
            _departmentReposytory = departmentReposytory;
        }

        [HttpPost("department/create")]
        public ActionResult<int> CreateDepartment([FromQuery] string description)
        {
            return Ok(_departmentReposytory.Create(new Department
            {
                Description = description
            }));
        }

        [HttpGet("department/getall")]
        public ActionResult<IList<CreateDepartmentRequest>> GetAllDepartment()
        {
            return Ok(_departmentReposytory.GetAll().Select(et =>
                new CreateDepartmentRequest
                {
                    Id = et.Id,
                    Description = et.Description
                }
                ).ToList());
        }

        [HttpGet("department/get-id")]
        public IActionResult GetByIdDepartment([FromQuery] int id)
        {
            return Ok(_departmentReposytory.GetById(id));
        }

        [HttpDelete("department/delete")]
        public ActionResult<bool> DeleteDepartment([FromQuery] int id)
        {
            return Ok(_departmentReposytory.Delete(id));
        }

        [HttpPut("department/update")]
        public ActionResult<bool> UpdateDepartment([FromQuery] Department item)
        {
            _departmentReposytory.Update(item);
            return Ok();
        }
    }
}
