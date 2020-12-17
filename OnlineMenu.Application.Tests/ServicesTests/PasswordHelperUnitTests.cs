using System.Security.Cryptography;
using OnlineMenu.Application.Services;
using Xunit;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Tests.ServicesTests
{
    public class PasswordHelper
    {
        [Fact]
        public void CreatePasswordHash_Test()
        {
            // Arrange
            const string password = "password";
            
            // Act
            var (passwordHashResult, passwordSaltResult) = PasswordHelpers.CreatePasswordHash(password);
            
            // Assert 
            var expectedPasswordHash = new HMACSHA512(passwordSaltResult).ComputeHash(UTF8.GetBytes(password));

            Assert.Equal(expectedPasswordHash, passwordHashResult);
        }
        
        [Theory]
        [InlineData("rightPassword", true)]
        [InlineData("wrongPassword", false)]
        public void VerifyPasswordHash_Test(string inputPassword, bool expectedResult)
        {
            // Arrange
            const string rightPassword = "rightPassword";
            var rightSalt = UTF8.GetBytes("SomePasswordSalt");
            var passwordHash = new HMACSHA512(rightSalt).ComputeHash(UTF8.GetBytes(rightPassword));
            
            // Act
            var result = PasswordHelpers.VerifyPasswordHash(inputPassword, passwordHash, rightSalt);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}