using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineMenu.Application.Services;
using OnlineMenu.Application.Services.Interfaces;
using Xunit;

namespace OnlineMenu.Application.Tests.ServicesTests.AdminServiceIntegrationTests
{
    
    public class AdminServiceIntegrationTests: IClassFixture<AdminSharedDatabaseFixture>
    {
        public AdminServiceIntegrationTests(AdminSharedDatabaseFixture fixture) => Fixture = fixture;
        private AdminSharedDatabaseFixture Fixture { get; }
        
        // [Fact]
        [Fact (Skip = "specific reason")]
        public async Task CreateAdmin_Test()
        {
            // Arrange
            var authenticationService = new Mock<IAuthenticationService>();

            const string username = "Valeriu";
            const string password = "Password";
                
            await using var transaction = await Fixture.Connection.BeginTransactionAsync();
            await using (var context = Fixture.CreateContext(transaction))
            {
                var service = new AdminService(context, authenticationService.Object);
                //Act
                await service.CreateAsync(username, password);
            }
            // Assert
            await using (var context = Fixture.CreateContext(transaction))
            {
                Assert.NotNull(await context.Admins.SingleOrDefaultAsync(a => a.UserName == username));
            }
        }

        [Fact]
        public async Task CreateAdmin_AlreadyExistingUsername_ArgumentException_Test()
        {
            // Arrange
            var existingUsername = DummyData.Admin.UserName;
            const string password = "Password";
            const string expectedExceptionMessage = "This username is already taken";

            await using var transaction = await Fixture.Connection.BeginTransactionAsync();
            await using var context = Fixture.CreateContext(transaction);
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            
            var service = new AdminService(context, authenticationServiceMock.Object);
            
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
                service.CreateAsync(existingUsername, password));
            
            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public async Task Authenticate_Test()
        {
            // Arrange
            var username = DummyData.Admin.UserName;
            const string password = DummyData.AdminPasswordString;
            const string expectedToken = "SomeToken";
            
            await using var transaction = await Fixture.Connection.BeginTransactionAsync();
            await using var context = Fixture.CreateContext(transaction);
            
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(authService =>
                authService.CreateToken(DummyData.AdminId, Roles.Admin))
                .Returns(expectedToken);
            
            var service = new AdminService(context, authenticationServiceMock.Object);
            
            // Act
            var token = await service.AuthenticateAsync(username, password);
            
            // Assert
            Assert.Equal(expectedToken, token);
        }
        
        [Fact]
        public async Task Authenticate_WrongPassword_AuthenticationException_Test()
        {
            // Arrange
            var userName = DummyData.Admin.UserName;
            const string wrongPassword = "SomeWrongPassword";
            const string expectedExceptionMessage = "Incorrect username or password";
            
            await using var transaction = await Fixture.Connection.BeginTransactionAsync();
            await using var context = Fixture.CreateContext(transaction);
            
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var service = new AdminService(context, authenticationServiceMock.Object);
            
            // Act & Assert
            var exception = await Assert.ThrowsAsync<AuthenticationException>(() => 
                service.AuthenticateAsync(userName, wrongPassword));
            
            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public async Task Authenticate_WrongUsername_AuthenticationException_Test()
        {
            // Arrange
            const string wrongUsername = "Some wrong Username";
            const string password = DummyData.AdminPasswordString;
            const string expectedExceptionMessage = "Incorrect username or password";
            
            await using var transaction = await Fixture.Connection.BeginTransactionAsync();
            await using var context = Fixture.CreateContext(transaction);
            
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var service = new AdminService(context, authenticationServiceMock.Object);
            
            // Act & Assert
            var exception = await Assert.ThrowsAsync<AuthenticationException>(() => 
                service.AuthenticateAsync(wrongUsername, password));
            
            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}