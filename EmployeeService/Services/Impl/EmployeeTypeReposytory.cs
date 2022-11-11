using EmployeeService.Data;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Services.Impl
{
    public class EmployeeTypeReposytory : IEmployeeTypeReposytory
    {
        #region Services

        private readonly EmployeeServiceDbContext _dbContext;

        #endregion

        public EmployeeTypeReposytory(EmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(EmployeeType data)
        {
            _dbContext.EmployeeTypes.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

        public EmployeeType GetById(int id)
        {
            return _dbContext.EmployeeTypes.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(EmployeeType data)
        {
            EmployeeType employeeType = GetById(data.Id);
            if (employeeType != null)
            {
                employeeType.Description = data.Description;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            EmployeeType employeeType = GetById(id);
            if (employeeType != null) 
            {
                _dbContext.EmployeeTypes.Remove(employeeType);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<EmployeeType> GetAll()
        {
            return _dbContext.EmployeeTypes.ToList();
        }
    }
}
