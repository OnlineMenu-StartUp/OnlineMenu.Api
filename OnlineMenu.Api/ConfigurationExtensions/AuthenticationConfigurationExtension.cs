using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Application;
using static System.Text.Encoding;

namespace OnlineMenu.Api.ConfigurationExtensions
{
    public static class AuthenticationConfigurationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string jwtKey)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.Admin, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
            });
            services.AddMvcCore().AddAuthorization();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.RequireHttpsMetadata = false;
                    bearerOptions.SaveToken = true;
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(ASCII.GetBytes(jwtKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}