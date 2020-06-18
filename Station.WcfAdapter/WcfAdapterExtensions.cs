using Microsoft.AspNetCore.Builder;

namespace Station.WcfAdapter
{
    public static class WcfAdapterExtensions
    {
        public static IApplicationBuilder UseWcfAdapter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WcfAdapterMiddleware>();
        }
    }
}