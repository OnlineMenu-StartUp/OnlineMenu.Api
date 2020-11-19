namespace OnlineMenu.Api.Constants
{
    public static class RegEx
    {
        /// <Summary>
        /// Password must be at least 6 characters and contain at 3 of 4 of the following:
        /// upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)
        /// </Summary>
        public const string PasswordValidation = 
            "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{6,}$";
    }
}