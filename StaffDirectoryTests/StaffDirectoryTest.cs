using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffDirectory;
using StaffDirectory.Data;
using StaffDirectory.Models;


namespace StaffDirectory.Tests
{
    [TestClass()]
    public class StaffDirectoryTest
    {

        private IServiceProvider _services;

        private IEmployeeRepo _repository;

        public void SetUp()
        {
            _services = Program.CreateHostBuilder(new string[] { }).Build().Services;
            ApplicationBuilder app = new ApplicationBuilder(_services);
            InMemoryDb.PrepPopulation(app);

        }

        [TestMethod()]
        public void GetAllEmployee()
        {
            SetUp();
            _repository = _services.GetRequiredService<IEmployeeRepo>();
            var employees = _repository.GetAllEmployee();
            var empResultCount = employees.Count();

            Assert.AreEqual(3, empResultCount);
        }
        [TestMethod()]
        public void CreateEmployee()
        {
            SetUp();
            _repository = _services.GetRequiredService<IEmployeeRepo>();

            Employee emp = new Employee();
            emp.Name = "Shirley";
            emp.Email = "Shirley@dummy.com";
            emp.Mobile = "0456897852";
            emp.Address = "30 Cooinda Street,Sydney, 2147, Australia";
            _repository.CreateEmployee(emp);
            _repository.SaveChanges();
            var employees = _repository.GetAllEmployee();
            var VerifyAddedEmp = employees.Where(e => e.Email == "Shirley@dummy.com");
            bool ActualResult = false;
            if (VerifyAddedEmp.Count() > 0)
            {
                ActualResult = true;
            }
            Assert.IsTrue(ActualResult);
        }
        [TestMethod()]
        public void UpdateEmployee()
        {
            SetUp();
            _repository = _services.GetRequiredService<IEmployeeRepo>();

            Employee emp = new Employee();
            var ActualEmp = _repository.GetAllEmployee().Where(e=>e.StaffId==1).FirstOrDefault();
            var ActualEmpHash = ActualEmp.GetHashCode();
            ActualEmp.Email = "john@dummmy.com";
            ActualEmp.Mobile = "0256897452";
            ActualEmp.Address = "30 Cooinda Street,Sydney, 2147, Australia";
            _repository.UpdateEmployee(emp);
            _repository.SaveChanges();
            var UpdatedEmployee = _repository.GetAllEmployee().Where(e => e.StaffId == 1).GetHashCode();
            
            Assert.AreNotEqual(UpdatedEmployee,ActualEmp);
        }

        [TestMethod()]
        public void DeleteEmployee()
        {
            SetUp();
            _repository = _services.GetRequiredService<IEmployeeRepo>();

            Employee emp = new Employee();
            var ActualEmp = _repository.GetAllEmployee().Where(e => e.StaffId == 1).FirstOrDefault();
            
            _repository.DeleteEmployee(ActualEmp.StaffId);
            _repository.SaveChanges();
            var UpdatedEmployee = _repository.GetAllEmployee().Where(e => e.StaffId == 1).FirstOrDefault();
            int deleted = 0;
            if (UpdatedEmployee == null)
            {
                deleted = 1;
            }

            Assert.AreEqual(1,deleted);
        }



    }
}
