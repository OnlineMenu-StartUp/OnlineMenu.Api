// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace OnlineMenu.Api.ViewModel.Authentication
{
    public class AuthenticateRequestModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}