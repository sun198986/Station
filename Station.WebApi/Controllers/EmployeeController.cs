using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
using Station.Models.EmployeeDto;
using Station.Repository.StaionRegist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist/{registId}/employee")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,IRegistRepository registRepository, IMapper mapper)
        {
            this._employeeRepository = employeeRepository??throw new ArgumentNullException(nameof(employeeRepository));
            this._registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForRegist(string registId,EmployeeAddDto employee) {

            if (string.IsNullOrWhiteSpace(registId)) {
                throw new ArgumentNullException(nameof(registId));
            }

            if (!await _registRepository.RegistExistsAsync(registId))
            {
                return NotFound();
            }

            return NotFound();
        }

        
    }
}
