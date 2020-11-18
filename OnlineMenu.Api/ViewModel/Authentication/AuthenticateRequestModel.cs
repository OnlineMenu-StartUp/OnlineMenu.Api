using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Api.ViewModel.Authentication
{
    public class AuthenticateRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}