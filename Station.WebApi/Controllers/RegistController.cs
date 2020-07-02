using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Station.Aop.Filter;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.RegistDto;
using Station.Repository;
using Station.Repository.StaionRegist;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController : ControllerBase
    {
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RegistController(IRegistRepository registRepository, IMapper mapper,ILogger<RegistController> logger)
        {
            _registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetRegists([FromQuery] string fields)
        {
            var entities = await _registRepository.GetRegistsAsync();
            var listDto = _mapper.Map<IEnumerable<RegistDto>>(entities);
            return Ok(listDto.ShapeData(fields));
        }

        [HttpGet("{ids}", Name = nameof(GetRegistCollection))]
        public async Task<IActionResult> GetRegistCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<string> ids, [FromQuery] string fields)
        {
            if (ids == null)
                return BadRequest();

            var entities = await _registRepository.GetRegistsAsync(ids);

            if (ids.Count() != entities.Count())
            {
                List<string> idNotFounds = ids.Where(x => !entities.Select(p => p.RegistId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

            var listDto = _mapper.Map<IEnumerable<RegistDto>>(entities);
            return Ok(listDto.ShapeData(fields));
        }

        [HttpPost]
        public IActionResult CreateReigst(RegistAddDto regist)
        {
            if (regist == null)
                throw new ArgumentNullException(nameof(regist));

            var entity = _mapper.Map<Regist>(regist);
            _registRepository.AddRegist(entity);
            _registRepository.SaveChanges();
            var returnDto = _mapper.Map<RegistDto>(entity);
            return CreatedAtRoute(nameof(GetRegistCollection), new { ids = returnDto.RegistId }, returnDto);
        }

        [HttpDelete("{ids}")]
        public async Task<IActionResult> DeleteRegist(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<string> ids)
        {
            if (ids == null)
                return BadRequest();

            var entities = await _registRepository.GetRegistsAsync(ids);
            if (ids.Count() != entities.Count())
            {
                List<string> idNotFounds = ids.Where(x => !entities.Select(p => p.RegistId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

            _registRepository.DeleteRegist(entities);
            _registRepository.SaveChanges();

            return NoContent();
        }

        [HttpPut("{registId}")]
        public async Task<IActionResult> UpdateRegist([FromRoute]string registId,[FromBody]RegistUpdateDto regist)
        {
            if (registId == null)
                throw new ArgumentNullException(nameof(registId));
            if (regist == null)
                throw new ArgumentNullException(nameof(regist));
            var entity = await _registRepository.GetRegistsAsync(registId);
            if (entity == null)
            {
                return NotFound($"id:{registId}没有查到数据");
            }

            _mapper.Map(regist, entity);
            _registRepository.UpdateRegist(entity);
            _registRepository.SaveChanges();
            return NoContent();
        }
    }
}