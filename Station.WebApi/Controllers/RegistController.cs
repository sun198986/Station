using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Station.Entity.DB2Admin;
using Station.Helper;
using Station.Models.BaseDto;
using Station.Models.RegistDto;
using Station.Repository;
using Station.Repository.RepositoryPattern;
using Station.Repository.StaionRegist;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController : ControllerBase
    {
        private readonly IRegistRepository _registRepository;
        private readonly IMapper _mapper;

        public RegistController(IRegistRepository registRepository, IMapper mapper)
        {
            _registRepository = registRepository ?? throw new ArgumentNullException(nameof(registRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        /// <param name="search">查询条件 例: Name:孙,Age:1</param>
        /// <param name="fields">塑形字段</param>
        /// <param name="sort">排序字段 orderByField</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRegists(
            [FromQuery,ModelBinder(BinderType = typeof(DtoModelBinder<RegistSearchDto>))] RegistSearchDto search,
            [FromQuery] string fields,
            [FromQuery] Sort sort
        )
        {
            Expression<Func<Regist, bool>> expression = null;
            if (search != null)
            {
                var entity = _mapper.Map<Regist>(search);
                //Expression<Func<Regist, bool>> expression = m=>m.Phone=="123";
                expression = entity.AsExpression();
            }

            var entities = await _registRepository.GetAsync(expression,sort);
            var listDto = _mapper.Map<IEnumerable<RegistDto>>(entities);
            return Ok(listDto.ShapeData(fields));
        }

        /// <summary>
        /// 按id的集合查询注册信息
        /// </summary>
        /// <param name="ids">id的集合 例:1,2,3,4</param>
        /// <param name="fields">塑形字段</param>
        /// <returns></returns>
        [HttpGet("{ids}", Name = nameof(GetRegistCollection))]
        public async Task<IActionResult> GetRegistCollection(
            [FromRoute,ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids, 
            [FromQuery] string fields)
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
    }
}