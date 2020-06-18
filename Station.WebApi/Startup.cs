using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceReference;
using Station.Aop.Filter;
using Station.Entity.DB2Admin;
using Station.Repository.StaionRegist;
using Station.Repository.StaionRegist.Implementation;
using Station.WcfAdapter;

namespace Station.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomerExceptionFilterAttribute));//全局异常处理
            })
            .AddXmlDataContractSerializerFormatters();//支持xml处理

            services.AddDbContext<Db2AdminDbContext>();
            services.AddTransient<IRegistRepository, RegistRepository>();
            //services.AddScoped<ClientBase<IUser>,UserClient>(new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, remoteAddress: @"http://10.236.198.102:8888/ServiceControler/User"));


            services.AddAutoMapper(Assembly.Load("Station.Entity"),Assembly.Load("Station.Models"));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            //app.UseWcfAdapter();//wcf中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
