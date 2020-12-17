namespace OnlineMenu.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string CreateToken(int userId, string role);
    }
}