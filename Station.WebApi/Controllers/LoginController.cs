using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReference;
using Station.Models.UserDto;
using Station.Repository.Login;

namespace Station.WebApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController:ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository??throw new ArgumentNullException(nameof(loginRepository));
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(UserDto user)
        {
            UserInfo userInfo = await _loginRepository.Login(user.UserName, user.Password);
            if (userInfo?.UserName != null && userInfo.Status == UserInfo.StatusType.正常)
            {
                string myToken = await _loginRepository.GetToken(userInfo.UserName, DateTime.Now, DateTime.Now.AddDays(1));
                user.MyToken = myToken;
                user.LoginResult = true;
            }
            else
            {
                user.LoginResult = false;
            }

            return Ok(user);
        }
    }
}