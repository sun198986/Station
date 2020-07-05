using System.Linq;
using System.Reflection;
using AutoMapper;
using IBM.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Station.Aop.Filter;
using Station.EFCore.IbmDb;

namespace Station.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static readonly ILoggerFactory ConsoleLoggerFactory =
           LoggerFactory.Create(builder =>
           {
               builder.AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information).AddConsole();
           });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomerExceptionFilterAttribute));//ȫ���쳣����
                options.Filters.Add(typeof(CustomerResultFilterAttribute));
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setup =>//������Ϣ������� FluentValidation
            {
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "",
                        Title = "�д���",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "�뿴��ϸ��Ϣ",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });//֧��xml����

            services.AddDbContext<IbmDbContext>(options =>
            {
                options.UseDb2(Configuration.GetConnectionString("DevelopDB"), action =>
                {

                }).UseLoggerFactory(ConsoleLoggerFactory);//��ӡsql�ű�
            });

            services.Scan(scan => scan.FromAssemblies(Assembly.Load("Station.Repository"))
               .AddClasses().UsingAttributes());//����ע��
            // services.AddTransient<IRegistRepository, RegistRepository>();
            // services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<ClientBase<IUser>,UserClient>(new UserClient(UserClient.EndpointConfiguration.WSHttpBinding_IUser, remoteAddress: @"http://10.236.198.102:8888/ServiceControler/User"));


            services.AddAutoMapper(config =>
            {
                config.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
            }, Assembly.Load("Station.Entity"), Assembly.Load("Station.Models"));

            services.Configure<MvcOptions>(config =>
            {
                var newtonSoftJsonOutputFormatter =
                    config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
            });
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

            //app.UseWcfAdapter();//wcf�м��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
