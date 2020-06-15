using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Station.Repository.StaionRegist;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController : ControllerBase
    {
        private readonly IRegistRepository _registRepository;

        public RegistController(IRegistRepository registRepository)
        {
            _registRepository = registRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegists()
        {
            var list = await _registRepository.GetRegistsAsync();
            return Ok(list);
        }
    }
}