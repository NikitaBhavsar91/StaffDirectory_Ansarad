using StaffDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffDirectory.Data
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeAppDbContext _context;
        public EmployeeRepo(EmployeeAppDbContext context)
        {
            _context = context;
        }
        public void CreateEmployee(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException(nameof(emp));
            }

            _context.Employees.Add(emp);
        }

        public void DeleteEmployee(int staffId)
        {
            _context.Remove(_context.Employees.Single(a => a.StaffId == staffId));
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

       
        public void UpdateEmployee(Employee emp)
        {
            _context.Update(emp);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

       
    }
}
