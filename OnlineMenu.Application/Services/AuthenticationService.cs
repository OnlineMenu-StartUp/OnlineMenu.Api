using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Application.Services.Interfaces;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Exceptions;
using static System.DateTime;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly AppSettings appSettings;
        
        public AuthenticationService(IOptions<AppSettings> appSettingsOptions)
        {
            appSettings = appSettingsOptions.Value;
        }
        
        public string CreateToken(int userId, string role)
        {
            var jwtKey = appSettings.Secrets?.JwtKey;

            if (string.IsNullOrWhiteSpace(jwtKey)) throw new ConfigurationException(nameof(jwtKey));
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = UtcNow.AddHours(appSettings.JwtExpirationTimeHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return tokenString;
        }
    }
}