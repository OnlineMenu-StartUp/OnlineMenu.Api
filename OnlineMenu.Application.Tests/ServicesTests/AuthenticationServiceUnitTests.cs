using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Exceptions;
using Xunit;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Tests.ServicesTests
{
    public class AuthenticationServiceUnitTests
    {
        [Theory]
        [InlineData(1, "jnasdjsadkajnknnsn", 1, "admin")]
        [InlineData(1, "adasdasdjnjnkamomo", 2,"cook")]
        [InlineData(2, "bnjkllmdjknknamomo", 4,"customer")]
        public void CreateToken_Test(int timeoutHours, string jwtKey, int userId, string role)
        {
            // Arrange
            var someOptions = Options.Create(new AppSettings
            {
                JwtExpirationTimeHours = timeoutHours,
                Secrets = new Secrets { JwtKey = jwtKey }
            });
            var service = new AuthenticationService(someOptions);

            // Act
            var response = service.CreateToken(userId, role);
            
            // Assert
            Assert.NotEmpty(response);
            
            new JwtSecurityTokenHandler().ValidateToken(response, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(ASCII.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);
            var jwtToken = (JwtSecurityToken) validatedToken;
            
            Assert.Equal(userId, int.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value));
            Assert.Equal(role, jwtToken.Claims.First(claim => claim.Type == "role").Value);
        }
        
        [Fact]
        public void CreateToken_NoJwtKey_ThrowConfigurationError_Test()
        {
            // Arrange
            var someOptions = Options.Create(new AppSettings
            {
                JwtExpirationTimeHours = 1,
                Secrets = new Secrets { JwtKey = "" }
            });
            var service = new AuthenticationService(someOptions);
            const int userId = 1;
            const string role = "admin";
            const string expectedExceptionMessage = "jwtKey";
;            
            // Act & Assert
            var exception = Assert.Throws<ConfigurationException>(() => service.CreateToken(userId, role));
            
            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}