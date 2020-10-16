using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Api;
using static System.DateTime;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Services
{
    public class AuthenticationService
    {
        private readonly AppSettings appSettings;
        
        public AuthenticationService(IOptions<AppSettings> appSettingsOptions)
        {
            appSettings = appSettingsOptions.Value;
        }
        
        public string CreateToken(string claim, string role)
        {
            var jwtKey = appSettings.Secrets.JwtKey;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, claim),
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
        
        public static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(); // Randomly generates a key (salt)
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }
        
        public static bool VerifyPasswordHash(string password, byte[] storedPasswordHash, byte[] storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(UTF8.GetBytes(password));

            for (var i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != storedPasswordHash[i]) return false;
            return true;
        }
    }
}