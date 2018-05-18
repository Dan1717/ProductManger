using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManager.DataAccess;
using AutoMapper;
using ProductsManager.Services.CategoryService;
using Swashbuckle.AspNetCore.Swagger;
using ProductsManager.Services.ProductServices;
using Microsoft.IdentityModel.Tokens;
using ProductsManager.WebApi.Controllers;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

namespace ProductsManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        private IHostingEnvironment CurrentEnvironment
        {
            get;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddOptions();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{CurrentEnvironment.EnvironmentName}.json", true, true);
            var config = builder.Build();
            services.Configure<DatabaseOptions>((o) =>
            {
                o.ConnectionString = config.GetSection("DefaultConnection:ConnectionString").Value;
            });
            services.UseDataAccessLayer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddAutoMapper();
            Mapper.AssertConfigurationIsValid();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = TokenController.SIGNING_KEY,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            app.UseAuthentication();
            app.UseMvc();

        }

    }

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

    public static class LoggingMidlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
   
}
