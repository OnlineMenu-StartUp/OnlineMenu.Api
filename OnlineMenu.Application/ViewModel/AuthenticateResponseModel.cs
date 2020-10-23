namespace OnlineMenu.Application.ViewModel
{
    public class AuthenticateResponseModel
    {
        public AuthenticateResponseModel(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
        public string UserName { get; }
        public string Token { get; }
    }
}