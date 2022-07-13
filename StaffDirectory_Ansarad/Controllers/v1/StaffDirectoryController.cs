using Microsoft.AspNetCore.Mvc;
using StaffDirectory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StaffDirectory.Dtos;
using StaffDirectory.Models;
using Microsoft.AspNetCore.Routing;
using StaffDirectory.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace StaffDirectory.WebApi.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
     public class StaffDirectoryController : ControllerBase
    {
        private readonly IEmployeeRepo _repository;
        private readonly IMapper _mapper;
        private readonly IJwtAuth _jwtAuth;

        public StaffDirectoryController(IEmployeeRepo repository,IMapper mapper, IJwtAuth jwtAuth)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtAuth = jwtAuth;
        }
        [AllowAnonymous]
        // POST api/<StaffDirectoryController>
        [HttpPost("authentication")]
        public ActionResult Authentication(UserCredential userCredential)
        {
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpGet]
        [Route("GetStaffDirectory")]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetStaffDirectory()
        {
            var employeeItem = _repository.GetAllEmployee();
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employeeItem));
        }
        [HttpGet]
        [Route("GetEmployeeByStaffId")]
        public ActionResult<EmployeeReadDto> GetEmployeeByStaffId(int staffId)
        {
            var employeeItem =  _repository.GetAllEmployee().Where(x => x.StaffId == staffId).FirstOrDefault();
            if (employeeItem != null)
            {
                return Ok(_mapper.Map<EmployeeReadDto>(employeeItem));
            }

            return NotFound();
        }
        [HttpPost]
        [Route("CreateEmployee")]
        public ActionResult<EmployeeReadDto> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
            _repository.CreateEmployee(employeeModel);
            _repository.SaveChanges();

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);
            if (employeeReadDto.StaffId != 0)
            {
                
                return Ok(_mapper.Map<EmployeeReadDto>(employeeReadDto));
            }
            return StatusCode(500);
            
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public ActionResult<EmployeeReadDto> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
        {
           
             var employeeModel = _mapper.Map<Employee>(employeeUpdateDto);
            _repository.UpdateEmployee(employeeModel);
            _repository.SaveChanges();

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

            if (employeeReadDto != null)
            {

                return Ok(_mapper.Map<EmployeeReadDto>(employeeReadDto));
            }
            

            return StatusCode(500);
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public ActionResult DeleteEmployee(int staffId)
        {
            _repository.DeleteEmployee(staffId);
            bool res=_repository.SaveChanges();
            if (res)
            {
                return Ok();
            }
            return StatusCode(500);
        }

    }
}
