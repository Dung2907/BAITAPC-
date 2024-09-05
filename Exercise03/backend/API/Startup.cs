using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;  // Thêm dòng này
//using API.Common.Helper;
using TranAnhDung.API.DataAccess.Context;
using API.Services;
using TranAnhDung.API.Services.Interface;  // Cho IEmployee
using TranAnhDung.API.Services;
using TranAnhDung.API.Common.Helper;
//using API.Services.Interface;

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
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddSingleton<ApplicationSettings>();
            // Cấu hình Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exercise04 API", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((hFost) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddDbContext<EFDataContext>(options =>
            options.UseSqlServer(
             Configuration.GetConnectionString("HRConnectString"),
                b => b.MigrationsAssembly("API.DataAccess")));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exercise04 V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // Đảm bảo chuyển hướng HTTPS
            app.UseStaticFiles();      // Cấu hình để phục vụ các tệp tĩnh

            app.UseRouting();

            app.UseCors("CorsPolicy"); // Đảm bảo CORS được cấu hình chính xác

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Định tuyến các controller
            });
        }


    }
}