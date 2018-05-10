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

namespace ProductsManager
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
            services.AddMvc();
            services.AddOptions();
            services.Configure<DatabaseOptions>(Configuration.GetSection("DefaultConnection"));
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
                    //ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
	        app.UseSwagger();
	        app.UseSwaggerUI(c =>
	        {
		        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	        });

			app.UseMvc();

            app.UseAuthentication();
        }
    }

   
}
