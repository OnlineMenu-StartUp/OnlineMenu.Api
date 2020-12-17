using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application.Services.Interfaces;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class CookService
    {
        private readonly IOnlineMenuContext context;
        private readonly IAuthenticationService authenticationService;

        public CookService(IOnlineMenuContext context, IAuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        
        public async Task CreateAsync(string userName, string password)
        {
            if (await context.Cooks.AnyAsync(c => c.UserName == userName))
                throw new ArgumentException("This username is already taken");

            var (passwordHash, passwordSalt) = PasswordHelpers.CreatePasswordHash(password);
            var newCook = new Cook { UserName = userName, Password = passwordHash, PasswordSalt = passwordSalt };

            context.Cooks.Add(newCook);
            await context.SaveChangesAsync();
        }

        public async Task<string> AuthenticateAsync(string userName, string password)
        {
            var cookFromDb = await context.Cooks.SingleOrDefaultAsync(c => c.UserName == userName);
            if (cookFromDb == null)
                throw new AuthenticationException("Incorrect user name or password");
            
            if (!PasswordHelpers.VerifyPasswordHash(password, cookFromDb.Password, cookFromDb.PasswordSalt))
                throw new AuthenticationException();

            return authenticationService.CreateToken(cookFromDb.Id, Roles.Cook);
        }
    }
}