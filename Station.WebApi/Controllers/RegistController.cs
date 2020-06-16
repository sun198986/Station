using System;
using System.Collections;
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

        [HttpGet("{ids}", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection(
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
    }
}