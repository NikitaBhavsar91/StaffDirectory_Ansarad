using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffDirectory.Dtos
{
    public class EmployeeReadDto
    {       
        public int StaffId { get; set; }  
        public string Name { get; set; }       
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
