using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StaffDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffDirectory.Data
{
    public static class InMemoryDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<EmployeeAppDbContext>());
            }
        }
        private static void SeedData(EmployeeAppDbContext context)
        {
           
            if (!context.Employees.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Employees.AddRange(
                    new Employee() { Name = "John", Email="john.smith@dummmy.com",Mobile="9898989656",Address= "98 Shirley Street PIMPAMA QLD 4209" },
                    new Employee() { Name = "Garry", Email = "Garry.Cole@dummmy.com", Mobile = "0457893652", Address = "45 Muscat Street PIMPAMA QLD 4209" },
                    new Employee() { Name = "Abhi", Email = "Abhi.abs@dummmy.com", Mobile = "0255889966", Address = "17 Denison Road PIMPAMA QLD 4209" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
