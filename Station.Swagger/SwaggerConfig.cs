using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace Station.Swagger
{
    public static class SwaggerConfig
    {
        public static void InitSwaggerConfig(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Station API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Station.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UserSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Station API V1");
            });
        }
    }
}