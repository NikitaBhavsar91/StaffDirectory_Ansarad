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
using Microsoft.AspNetCore.Authorization;
using StaffDirectory.Authentication;

namespace StaffDirectory.WebApi.v2
{
    [Authorize]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/[controller]")]
   
    public class StaffDirectoryController : StaffDirectory.WebApi.v1.StaffDirectoryController
    {
        private readonly IEmployeeRepo _repository;
        private readonly IMapper _mapper;
        private readonly IJwtAuth _jwtAuth;
        public StaffDirectoryController(IEmployeeRepo repository, IMapper mapper, IJwtAuth jwtAuth) : base(repository,mapper,jwtAuth)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtAuth = jwtAuth;
        }
        [HttpGet]
        [Route("GetEmployeeByName")]
        [MapToApiVersion("2")]
        public ActionResult <IEnumerable<EmployeeReadDto>> GetEmployeeByName(string name)
        {
            var employeeItem = _repository.GetAllEmployee().Where(x=>x.Name==name);
            if (employeeItem != null)
            {
                return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employeeItem));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("GetEmployeeByEmail")]
        [MapToApiVersion("2")]
        public ActionResult<EmployeeReadDto> GetEmployeeByEmail(string email)
        {
            var employeeItem = _repository.GetAllEmployee().Where(x => x.Email == email).FirstOrDefault();
            if (employeeItem != null)
            {
                return Ok(_mapper.Map<EmployeeReadDto>(employeeItem));
            }

            return NotFound();
        }
        

    }
}
