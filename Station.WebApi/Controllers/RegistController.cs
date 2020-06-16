using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.RegistDto;
using Station.Repository.StaionRegist;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController : ControllerBase
    {
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;

        public RegistController(IRegistRepository registRepository,IMapper mapper)
        {
            _registRepository = registRepository??throw new ArgumentNullException(nameof(registRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetRegists([FromQuery] string fields)
        {
            var entities = await _registRepository.GetRegistsAsync();
            var listDto =_mapper.Map<IEnumerable<RegistDto>>(entities);
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
        public async Task<IActionResult> AddReigst(RegistAddDto regist)
        {
            if(regist==null)
                throw new ArgumentNullException(nameof(regist));

            var entity =  _mapper.Map<Regist>(regist);
            _registRepository.AddRegist(entity);
            await _registRepository.SaveAsync();//异步问题待解决

            var returnDto = _mapper.Map<RegistDto>(entity);
            return CreatedAtRoute(nameof(GetRegistCollection), new {ids = returnDto.RegistId}, returnDto);

        }
    }
}