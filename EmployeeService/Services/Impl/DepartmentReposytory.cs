using EmployeeService.Data;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Services.Impl
{
    public class DepartmentReposytory : IDepartmentReposytory
    {
        private readonly EmployeeServiceDbContext _dbContext;

        public DepartmentReposytory(EmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Department data)
        {
            _dbContext.Departments.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

        public bool Delete(int id)
        {
            Department department = GetById(id);
            if (department != null)
            {
                _dbContext.Departments.Remove(department);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(et => et.Id == id);
        }

        public bool Update(Department data)
        {
            Department department = GetById(data.Id);
            if (department != null)
            {
                department.Description = data.Description;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
