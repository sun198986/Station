using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Station.Core.Filter
{
    public class CustomerActionFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //api的验证
            Console.WriteLine($"This is {typeof(CustomerActionFilterAttribute)} OnActionExecuted");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomerActionFilterAttribute)} OnActionExecuting");
        }
    }
}