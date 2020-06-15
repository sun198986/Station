using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Station.Entity.DB2Admin;
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
        public async Task<IActionResult> GetRegists()
        {
            var list = await _registRepository.GetRegistsAsync();
            _mapper.Map<RegistDto>(list);
            return Ok(list);
        }
    }
}