using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceReference;
using Station.WcfAdapter;

namespace Station.Core.Authorization
{
    public class CustomerAuthorizeFilterAttribute: Attribute,IAsyncAuthorizationFilter, IFilterMetadata
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IWcfAdapter _wcfAdapter;

        public CustomerAuthorizeFilterAttribute(IApplicationContext applicationContext,IWcfAdapter wcfAdapter)
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
                        string currentUser = context.HttpContext.Session.GetString(myToken);
                        _applicationContext.CurrentUser =
                            System.Text.Json.JsonSerializer.Deserialize<UserInfo>(currentUser);
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