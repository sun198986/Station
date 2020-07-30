using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceReference;
using IUserRoleControl = Station.Core.UserRoleWcf.IUserRoleControl;

namespace Station.Core.Authorization
{
    public class CustomerAuthorizeFilterAttribute: Attribute,IAsyncAuthorizationFilter, IFilterMetadata
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IUserRoleControl _wcfAdapter;

        public CustomerAuthorizeFilterAttribute(IApplicationContext applicationContext,IUserRoleControl wcfAdapter)
        {
            _applicationContext = applicationContext;
            _wcfAdapter = wcfAdapter;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor)
                .MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                string myToken = context.HttpContext.Request.Headers["token"];
                if (!string.IsNullOrEmpty(myToken))
                {
                    TokenClient tokenClient = _wcfAdapter.GetTokenClient();
                    //验证令牌
                    try
                    {
                        await tokenClient.ValidateGuidAsync(myToken);
                        _applicationContext.SetCurrentUserLogInfo(myToken);
                    }
                    catch (System.Exception e)
                    {
                        context.Result = new UnauthorizedObjectResult(e.Message);
                    }
                }
            }
        }
    }
}