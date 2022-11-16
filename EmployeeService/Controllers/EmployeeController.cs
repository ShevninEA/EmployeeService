using Azure.Core;
using EmployeeService.Data;
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
        public ActionResult<int> CreateEmployee([FromQuery] CreateEmployeeRequest request)
        {
            return Ok(_employeeReposytory.Create(new Employee
            {
                DepartmentId = request.DepartmentId,
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Salary = request.Salary               
            })); ;
        }

        [HttpGet("employee/getall")]
        public ActionResult<IList<CreateEmployeeRequest>> GetAllEmployee()
        {
            return Ok(_employeeReposytory.GetAll().Select(et =>
                new CreateEmployeeRequest
                {
                    Id = et.Id,
                    DepartmentId = et.DepartmentId,
                    EmployeeTypeId = et.EmployeeTypeId,
                    FirstName = et.FirstName,
                    Surname = et.Surname,
                    Patronymic = et.Patronymic,
                    Salary = et.Salary
                }
                ).ToList());
        }

        [HttpGet("employee/get-id")]
        public IActionResult GetByIdEmployee([FromQuery] int id)
        {
            return Ok(_employeeReposytory.GetById(id));
        }

        [HttpDelete("employee/delete")]
        public ActionResult<bool> DeleteEmployee([FromQuery] int id)
        {
            return Ok(_employeeReposytory.Delete(id));
        }

        [HttpPut("employee/update")]
        public ActionResult<bool> UpdateEmployee([FromQuery] Employee item)
        {
            _employeeReposytory.Update(item);
            return Ok();
        }
    }
}
