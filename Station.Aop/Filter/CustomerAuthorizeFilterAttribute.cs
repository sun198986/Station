using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceReference;

namespace Station.Aop.Filter
{
    public class CustomerAuthorizeFilterAttribute: Attribute,IAsyncAuthorizationFilter, IFilterMetadata
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor)
                .MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                string myToken = context.HttpContext.Request.Headers["token"];
                if (!string.IsNullOrEmpty(myToken))
                {
                    TokenClient tokenClient = WcfAdapter.WcfAdapterUtil.GetWcfClient<TokenClient>();
                    //验证令牌
                    try
                    {
                        await tokenClient.ValidateGuidAsync(myToken);
                        string currentUser = context.HttpContext.Session.GetString(myToken);

                    }
                    catch (Exception e)
                    {
                        context.Result = new UnauthorizedObjectResult(e.Message);
                    }
                }
            }
        }
    }
}