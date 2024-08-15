using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataAccess.Context;
using API.Services;
using API.Services.Interface;
using API.Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Registering services
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IDepartment, DepartmentService>();

            // Configuration settings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddScoped<ApplicationSettings>();

            // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Database context
            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ProductConnectionString"),
                    b => b.MigrationsAssembly("API")
                )
            );

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API Documentation"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger
            app.UseSwagger();

            // Enable Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root (localhost:<port>/)
            });

            app.UseRouting();

            // CORS middleware
            app.UseCors("CorsPolicy");

            // Authorization middleware
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
