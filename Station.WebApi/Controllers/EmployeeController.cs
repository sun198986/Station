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
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Station.Models.RegistDto;
using Station.Repository.RepositoryPattern;
using Station.SortApply.Helper;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist/{registId}/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public EmployeeController(IEmployeeRepository employeeRepository, IRegistRepository registRepository, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            this._employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this._registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployees(string id,[FromQuery] string fields) {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _employeeRepository.GetSingleAsync(id);
            var returnDto = _mapper.Map<IEnumerable<EmployeeDto>>(entity);
            return Ok(returnDto.ShapeData(fields));
        }

        [HttpGet(Name = nameof(GetEmployeeCollection))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeCollection(
            [FromQuery] EmployeeDtoParameter employeeDtoParameter)
        {
            Expression<Func<Employee, bool>> expression = null;

            if (employeeDtoParameter.Search != null)
            {
                var entity = _mapper.Map<Employee>(employeeDtoParameter.Search);
                //Expression<Func<Regist, bool>> expression = m=>m.Phone=="123";
                expression = entity.AsExpression();
            }

            Dictionary<string, PropertyMappingValue> mappingDictionary = null;

            if (employeeDtoParameter.OrderBy != null)
            {
                if (!_propertyMappingService.ValidMappingExistsFor<EmployeeDto, Employee>(employeeDtoParameter.OrderBy))
                {
                    return BadRequest("无法找到对应的属性");
                }

                mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();
            }

            var entities = await _employeeRepository.GetAsync(employeeDtoParameter.Ids, expression, employeeDtoParameter.OrderBy, mappingDictionary);
            if (employeeDtoParameter.Ids != null && employeeDtoParameter.Ids.Count() != entities.Count())
            {
                List<string> idNotFounds = employeeDtoParameter.Ids.Where(x => !entities.Select(p => p.RegistId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

            var listDto = _mapper.Map<IEnumerable<EmployeeDto>>(entities);
            return Ok(listDto.ShapeData(employeeDtoParameter.Fields));
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

        [HttpPost("batch")]
        public async Task<IActionResult> CreateEmployeeForRegist(string registId, IEnumerable<EmployeeAddDto> employees)
        {

            if (string.IsNullOrWhiteSpace(registId))
            {
                throw new ArgumentNullException(nameof(registId));
            }

            if (!await _registRepository.RegistExistsAsync(registId))
            {
                return NotFound();
            }

            var entities = _mapper.Map<IList<Employee>>(employees);
            foreach (var employee in entities)
            {
                employee.RegistId = registId;
            }
            _employeeRepository.Add(entities);
            _employeeRepository.SaveChanges();
            var returnDtos = _mapper.Map<IEnumerable<EmployeeDto>>(entities);
            var idsString = string.Join(",", returnDtos.Select(x => x.EmployeeId));
            return CreatedAtRoute(nameof(GetEmployeeCollection), new { ids = idsString }, returnDtos);
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
