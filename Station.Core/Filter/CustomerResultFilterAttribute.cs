using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Station.Core.Filter
{
    public class CustomerResultFilterAttribute:ResultFilterAttribute
    {

        private readonly ILogger _logger;
        public CustomerResultFilterAttribute(ILogger<CustomerResultFilterAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        //验证客户端是否已经获取了数据
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
    }
}