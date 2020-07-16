using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.EmployeeDto;
using Station.Repository;
using Station.Repository.StaionRegist;
using Station.Repository.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Station.Models.RegistDto;

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
            var entities = await _employeeRepository.GetAsync();
            var listDto = _mapper.Map<IEnumerable<EmployeeDto>>(entities);
            return Ok(listDto.ShapeData(fields));
        }

        [HttpGet("{ids}", Name = nameof(GetEmployeeCollection))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<string> ids, [FromQuery] string fields)
        {
            if (ids == null)
                return BadRequest();

            var entities = await _employeeRepository.GetAsync(ids);

            if (ids.Count() != entities.Count())
            {
                List<string> idNotFounds = ids.Where(x => !entities.Select(p => p.EmployeeId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

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

        [HttpDelete("{ids}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<string> ids) {
            if (ids == null)
                return BadRequest();

            var entities = await _employeeRepository.GetAsync(ids);

            if (ids.Count() != entities.Count())
            {
                List<string> idNotFounds = ids.Where(x => !entities.Select(p => p.EmployeeId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

            _employeeRepository.Delete(entities);
            _employeeRepository.SaveChanges();

            return NoContent();
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(string employeeId,[FromBody] EmployeeUpdateDto employee) {

            if (employee == null) {
                throw new ArgumentNullException(nameof(employee));
            }
            var entity = await _employeeRepository.GetSingleAsync(employeeId);
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
