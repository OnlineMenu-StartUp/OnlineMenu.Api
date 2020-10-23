using System;
using System.Linq;
using System.Security.Authentication;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class CookService
    {
        private readonly IOnlineMenuContext context;
        private readonly AuthenticationService authenticationService;

        public CookService(IOnlineMenuContext context, AuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        
        public void Create(string userName, string password)
        {
            // Input validation ---
            ValidateUserNameAndPassword(userName, password);

            if (context.Cooks.Any(x => x.UserName == userName))
                throw new ArgumentException("This username is already taken");
            // ---

            var (passwordHash, passwordSalt) = AuthenticationService.CreatePasswordHash(password);
            var newCook = new Cook { UserName = userName, Password = passwordHash, PasswordSalt = passwordSalt };

            context.Cooks.Add(newCook);
            context.SaveChanges();
        }

        public string Authenticate(string userName, string password)
        {
            ValidateUserNameAndPassword(userName, password);
            
            var cookFromDb = context.Cooks.SingleOrDefault(x => x.UserName == userName);
            if (cookFromDb == null)
                throw new AuthenticationException("Incorrect user name or password");
            
            if (!AuthenticationService.VerifyPasswordHash(password, cookFromDb.Password, cookFromDb.PasswordSalt))
                throw new AuthenticationException();

            return authenticationService.CreateToken(cookFromDb.Id.ToString(), Roles.Cook);
        }

        private static void ValidateUserNameAndPassword(string userName, string password)
        {
            // TODO: We can add here validations for username lenght, password complexity
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
        }
    }
}