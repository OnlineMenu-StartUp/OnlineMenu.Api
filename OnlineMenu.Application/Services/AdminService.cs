using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application.Services.Interfaces;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class AdminService
    {
        private readonly IOnlineMenuContext context;
        private readonly IAuthenticationService authenticationService;

        public AdminService(IOnlineMenuContext context, IAuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        
        public async Task CreateAsync(string userName, string password)
        {
            if (context.Admins.Any(x => x.UserName == userName))
                throw new ArgumentException("This username is already taken");

            var (passwordHash, passwordSalt) = PasswordHelpers.CreatePasswordHash(password);
            var newAdmin = new Admin {UserName = userName, Password = passwordHash, PasswordSalt = passwordSalt};

            context.Admins.Add(newAdmin);
            await context.SaveChangesAsync();
        }

        public async Task<string> AuthenticateAsync(string userName, string password)
        {
            var adminFromDb = await context.Admins.SingleOrDefaultAsync(x => x.UserName == userName);
            if (adminFromDb == null)
                throw new AuthenticationException("Incorrect username or password");
            
            if (!PasswordHelpers.VerifyPasswordHash(password, adminFromDb.Password, adminFromDb.PasswordSalt))
                throw new AuthenticationException("Incorrect username or password");

            return authenticationService.CreateToken(adminFromDb.Id, Roles.Admin);
        }
    }
}