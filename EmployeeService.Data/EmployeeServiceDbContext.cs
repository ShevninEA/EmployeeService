using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    public class EmployeeServiceDbContext : DbContext
    {
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AccountSession> AccountSession { get; set; }

        public EmployeeServiceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
