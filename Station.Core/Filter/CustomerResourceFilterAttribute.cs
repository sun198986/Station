using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Station.Core.Filter
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class CustomerResourceFilterAttribute: Attribute,IResourceFilter,IFilterMetadata,IOrderedFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public int Order { get; } = 1;
    }
}