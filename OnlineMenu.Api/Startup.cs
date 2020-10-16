using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OnlineMenu.Api.ExceptionHandling;
using OnlineMenu.Application;
using OnlineMenu.Domain.Exceptions;
using static System.Text.Encoding;

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

            // We can store it in the database, I dont know what's better/safer
            var jwtKey = Configuration.GetValue(typeof(string), "Secrets:JwtKey") as string;
            if (jwtKey == null) throw new ConfigurationException("JWT Key was not set");

            // configure jwt authentication
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(ASCII.GetBytes(jwtKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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
