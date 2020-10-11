namespace OnlineMenu.Application.ViewModel
{
    public class AuthenticateAdminResponseModel
    {
        public AuthenticateAdminResponseModel(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
        public string UserName { get; private set; }
        public string Token { get; private set; }
    }
}