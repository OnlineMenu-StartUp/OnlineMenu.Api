using System;
using System.Linq;
using System.Security.Authentication;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class AdminService
    {
        private readonly IOnlineMenuContext context;
        private readonly AuthenticationService authenticationService;

        public AdminService(IOnlineMenuContext context, AuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        
        public void Create(string userName, string password)
        {
            // Input validation ---
            ValidateUserNameAndPassword(userName, password);

            if (context.Admins.Any(x => x.UserName == userName))
                throw new ArgumentException("This username is already taken");
            // ---

            var (passwordHash, passwordSalt) = AuthenticationService.CreatePasswordHash(password);
            var newAdmin = new Admin {UserName = userName, Password = passwordHash, PasswordSalt = passwordSalt};

            context.Admins.Add(newAdmin);
            context.SaveChanges();
        }

        public string Authenticate(string userName, string password)
        {
            ValidateUserNameAndPassword(userName, password);
            
            var adminFromDb = context.Admins.SingleOrDefault(x => x.UserName == userName);
            if (adminFromDb == null)
                throw new AuthenticationException("Incorrect username or password");
            
            if (!AuthenticationService.VerifyPasswordHash(password, adminFromDb.Password, adminFromDb.PasswordSalt))
                throw new AuthenticationException("Incorrect username or password");

            return authenticationService.CreateToken(adminFromDb.Id.ToString(), Roles.Admin);
        }

        private static void ValidateUserNameAndPassword(string userName, string password)
        {
            // TODO: We can add here validations for username lenght, password complexity
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) 
                throw new ArgumentException("Username or password is missing");
        }
    }
}