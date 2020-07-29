using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.RegistDto;
using Station.Repository.RepositoryPattern;
using Station.Repository.StaionRegist;
using Station.SortApply.Helper;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController : ControllerBase
    {
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public RegistController(IRegistRepository registRepository, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService;
        }

        /// <summary>
        /// 查询单个注册信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="fields">塑形字段</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistDto>> GetRegist(string id,
            [FromQuery] string fields)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var entity = await _registRepository.GetSingleAsync(id);
            var returnDto = _mapper.Map<RegistDto>(entity);
            return Ok(returnDto.ShapeData(fields));
        }

        /// <summary>
        /// 查询注册信息
        /// </summary>
        /// <param name="registDtoP">查询条件 例: Name:孙,Age:1</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRegistCollection))]
        public async Task<IActionResult> GetRegistCollection(
            [FromQuery]RegistDtoParameter registDtoP
        )
        {
            Expression<Func<Regist, bool>> expression = null;

            if (registDtoP.Search != null)
            {
                var entity = _mapper.Map<Regist>(registDtoP.Search);
                expression = entity.AsExpression();
            }

            Dictionary<string, PropertyMappingValue> mappingDictionary = null;

            if (registDtoP.OrderBy != null)
            {
                if (!_propertyMappingService.ValidMappingExistsFor<RegistDto, Regist>(PropertyMappingConfig.PropertyMappings, registDtoP.OrderBy))
                {
                    return BadRequest("无法找到对应的属性");
                }
                mappingDictionary = _propertyMappingService.GetPropertyMapping<RegistDto, Regist>(PropertyMappingConfig.PropertyMappings);
            }

            var entities = await _registRepository.GetAsync(registDtoP.Ids, expression,registDtoP.OrderBy,mappingDictionary);
            if (registDtoP.Ids!=null && registDtoP.Ids.Count() != entities.Count())
            {
                List<string> idNotFounds = registDtoP.Ids.Where(x => !entities.Select(p => p.RegistId).ToList().Contains(x)).ToList();
                return NotFound(JsonSerializer.Serialize(idNotFounds));
            }

            var listDto = _mapper.Map<IEnumerable<RegistDto>>(entities);
            return Ok(listDto.ShapeData(registDtoP.Fields));
        }

        /// <summary>
        /// 创建注册信息
        /// </summary>
        /// <param name="regist">注册信息</param>
        /// <returns></returns>
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

        /// <summary>
        /// 批量创建注册信息
        /// </summary>
        /// <param name="regists">注册信息集合</param>
        /// <returns></returns>
        [HttpPost("batch")]
        public IActionResult CreateRegist(IEnumerable<RegistAddDto> regists)
        {
            if (!regists.Any())
            {
                throw new ArgumentNullException(nameof(regists));
            }
            var entities = _mapper.Map<IList<Regist>>(regists);
            _registRepository.Add(entities);
            _registRepository.SaveChanges();
            var returnDtos = _mapper.Map<IEnumerable<RegistDto>>(entities);
            var idsString = string.Join(",", returnDtos.Select(x => x.RegistId));

            return CreatedAtRoute(nameof(GetRegistCollection), new { ids = idsString }, returnDtos);
        }

        /// <summary>
        /// 删除单个注册信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteRegist(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var entity = await _registRepository.GetSingleAsync(id);
            if (entity == null)
            {
                return NotFound(nameof(id) + id);
            }

            _registRepository.Delete(entity);
            _registRepository.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// 根据id的集合删除注册信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        public async Task<IActionResult> DeleteRegists(
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

        /// <summary>
        /// 修改注册信息
        /// </summary>
        /// <param name="registId">主键id</param>
        /// <param name="regist">注册信息</param>
        /// <returns></returns>
        [HttpPut("{registId}")]
        public async Task<IActionResult> UpdateRegist([FromRoute] string registId, [FromBody] RegistUpdateDto regist)
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

        [HttpGet("includeEmployee/{registId}")]
        public async Task<IActionResult> GetRegistIncludeEmplyee(string registId)
        {
            if (registId == null)
                throw new ArgumentNullException(nameof(registId));
            var entity = await _registRepository.GetSingleRegistAndEmployeeAsync(registId);
            var returnDto = _mapper.Map<RegistDto>(entity);
            return Ok(returnDto);
        }
    }
}