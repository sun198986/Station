using Marvin.Cache.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Station.Core.ETag
{
    public static class ETagConfig
    {
        public static void InitEtagConfig(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expires =>
                {
                    expires.MaxAge = 120;
                    expires.CacheLocation = CacheLocation.Public;
                },
                validation =>
                {
                    validation.MustRevalidate = true;
                });
        }
    }
}