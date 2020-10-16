using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineMenu.Api.ConfigurationExtensions;
using OnlineMenu.Application.Services;
using OnlineMenu.Api.ExceptionHandling;
using OnlineMenu.Application;

namespace OnlineMenu.Api
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.ConfigureDbContext(Configuration.GetConnectionString("RemoteConnection"));
            
            // Add strongly typed AppSettings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var jwtKey = appSettingsSection.Get<AppSettings>().Secrets.JwtKey;

            services.ConfigureAuthentication(jwtKey);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.Admin, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
            });
            
            services.AddScoped<StatusService>();
            services.AddScoped<OrderService>();
            services.AddScoped<AdminService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<CookService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
//          app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionHandlingMiddleware>(env.IsDevelopment());
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
