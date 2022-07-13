using StaffDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffDirectory.Data
{
    public interface IEmployeeRepo
    {
        bool SaveChanges();

        IEnumerable<Employee> GetAllEmployee();
        void CreateEmployee(Employee emp);
        void UpdateEmployee(Employee emp);
        void DeleteEmployee(int staffId);
    }
}
