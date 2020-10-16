using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Api;
using OnlineMenu.Domain.Models;
using static System.DateTime;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Services
{
    public class AdminService
    {
        private readonly IOnlineMenuContext context;
        private readonly AppSettings appSettings;

        public AdminService(IOnlineMenuContext context, IOptions<AppSettings> appSettingsOptions)
        {
            this.context = context;
            appSettings = appSettingsOptions.Value;
        }
        
        public void Create(string userName, string password)
        {
            // Input validation ---
            ValidateUserNameAndPassword(userName, password);

            if (context.Admins.Any(x => x.UserName == userName))
                throw new ArgumentException("This username is already taken");
            // ---

            var (passwordHash, passwordSalt) = CreatePasswordHash(password);

            var newAdmin = new Admin {UserName = userName, Password = passwordHash, PasswordSalt = passwordSalt};

            context.Admins.Add(newAdmin);
            context.SaveChanges();
        }

        public string Authenticate(string userName, string password)
        {
            // Input validation ---
            ValidateUserNameAndPassword(userName, password);
            
            var adminFromDb = context.Admins.SingleOrDefault(x => x.UserName == userName);
            if (adminFromDb == null)
                throw new ArgumentException("Incorrect user name or password");
            // ---
            
            var jwtKey = appSettings.Secrets.JwtKey;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, adminFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Role, Roles.Admin)
                }),
                Expires = UtcNow.AddHours(appSettings.JwtExpirationTimeHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return tokenString;
        }

        private static void ValidateUserNameAndPassword(string userName, string password)
        {
            // TODO: We can add here validations for username lenght, password complexity
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) 
                throw new ArgumentException("Username or password is missing");
        }
        
        private static (string passwordHash, string passwordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(); // Randomly generates a key (salt)
            var passwordSalt = UTF8.GetString(hmac.Key);
            var passwordHash = UTF8.GetString(hmac.ComputeHash(UTF8.GetBytes(password)));
            return (passwordHash, passwordSalt);
        }
        
        private static bool VerifyPasswordHash(string password, string storedPasswordHash, string storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(UTF8.GetBytes(storedSalt));
            var computedHash = hmac.ComputeHash(UTF8.GetBytes(password));
            
            for (var i = 0; i < computedHash.Length; i++) // Comparing bit by bit, is a bit faster than comparing strings
                if (computedHash[i] != storedPasswordHash[i]) return false; // (pun intended)
            return true;
        }
    }
}