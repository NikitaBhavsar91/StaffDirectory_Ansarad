using Microsoft.EntityFrameworkCore;
using StaffDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffDirectory.Data
{
    public class EmployeeAppDbContext : DbContext
    {
        public EmployeeAppDbContext(DbContextOptions<EmployeeAppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
            
}
