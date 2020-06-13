using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Station.Businesses.StaionRegist;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/regist")]
    public class RegistController:ControllerBase
    {
        private readonly IRegistBusiness _registBusiness;

        public RegistController(IRegistBusiness registBusiness)
        {
            _registBusiness = registBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegists()
        {
            var list = await _registBusiness.GetRegistsAsync();
            return Ok(list);
        }
    }
}