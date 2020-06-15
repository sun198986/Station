using Microsoft.AspNetCore.Mvc.Filters;

namespace Station.Aop.Filter
{
    public class CustomerResultFilterAttribute:ResultFilterAttribute
    {
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