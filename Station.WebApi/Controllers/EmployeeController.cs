using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.EmployeeDto;
using Station.Repository;
using Station.Repository.StaionRegist;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist/{registId}/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IRegistRepository registRepository, IMapper mapper)
        {
            this._employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this._registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDto>> GetEmployees([FromQuery] string fields) {
            var entities = await _employeeRepository.GetAsync<IEmployeeRepository, Employee>();
            var listDto = _mapper.Map<IEnumerable<EmployeeDto>>(entities);
            return Ok(listDto.ShapeData(fields));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForRegist(string registId, EmployeeAddDto employee) {

            if (string.IsNullOrWhiteSpace(registId)) {
                throw new ArgumentNullException(nameof(registId));
            }

            if (!await _registRepository.RegistExistsAsync(registId))
            {
                return NotFound();
            }

            Employee entity = _mapper.Map<Employee>(employee);
            entity.EmployeeId = Guid.NewGuid().ToString();
            entity.RegistId = registId;
            _employeeRepository.Add(entity);
            _employeeRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId) {
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            var entity = await _employeeRepository.GetAsync<IEmployeeRepository, Employee>(employeeId);

            if (entity == null)
            {
                return NotFound(nameof(employeeId));
            }

            _registRepository.Delete(entity);
            _registRepository.SaveChanges();

            return NoContent();
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(string employeeId,[FromBody] EmployeeUpdateDto employee) {

            if (employee == null) {
                throw new ArgumentNullException(nameof(employee));
            }
            var entity = await _employeeRepository.GetAsync<IEmployeeRepository, Employee>(employeeId);
            if (entity == null) {
                return NotFound($"id:{employeeId}没有查到数据");
            }

            _mapper.Map(employee, entity);

            _employeeRepository.Update(entity);
            _employeeRepository.SaveChanges();
            return NoContent();
        }
    }
}
