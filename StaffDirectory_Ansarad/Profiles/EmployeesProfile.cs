using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StaffDirectory.Dtos;
using StaffDirectory.Models;

namespace StaffDirectory.Profiles
{
    public class EmployeesProfile:Profile
    {
        // Source -> Target
        public EmployeesProfile()
            {
                CreateMap<Employee, EmployeeReadDto>();
                CreateMap<EmployeeCreateDto, Employee>();
                CreateMap<EmployeeUpdateDto, Employee>();
            }
            
    }
}
