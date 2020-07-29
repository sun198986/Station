using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Station.Core.Exception
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class CustomerExceptionFilterAttribute:ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        public CustomerExceptionFilterAttribute(ILogger<CustomerExceptionFilterAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                _logger.LogError($"{context.Exception.Message}||{context.Exception.StackTrace}",context.Exception);
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                Console.WriteLine($"{context.Exception.Message}");
            }
        }
    }
}