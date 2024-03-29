﻿using EmployeeService.Data;
using EmployeeService.Models;

namespace EmployeeService.Services.Impl
{
    public class EmployeeReposytory : IEmployeeReposytory
    {
        private readonly EmployeeServiceDbContext _dbContext;

        public EmployeeReposytory(EmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Employee data)
        {
            _dbContext.Employees.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

        public bool Delete(int id)
        {
            Employee employees = GetById(id);
            if (employees != null)
            {
                _dbContext.Employees.Remove(employees);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(et => et.Id == id);
        }

        public bool Update(Employee data)
        {
            Employee employees = GetById(data.Id);
            if (employees != null)
            {
                _dbContext.Employees.Update(employees);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
