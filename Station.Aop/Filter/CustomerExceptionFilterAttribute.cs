using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Station.Aop.Filter
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class CustomerExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (!context.ExceptionHandled)
            {
                Console.WriteLine($"{context.Exception.Message}");
            }
        }
    }
}