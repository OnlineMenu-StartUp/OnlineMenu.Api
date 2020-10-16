using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace OnlineMenu.Api.ConfigurationExtensions
{
    public static class AuthenticationConfigurationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string jwtKey)
        {
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
        }
    }
}