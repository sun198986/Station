using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceReference;

namespace Station.WcfAdapter
{
    public class WcfAdapterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public WcfAdapterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<WcfAdapterMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
           // _logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());

            await _next.Invoke(context);
        }
    }
}