using System.ComponentModel.DataAnnotations;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OnlineMenu.Api.ViewModel.Authentication
{
    public class RegisterModel
    {
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string UserName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(Constants.RegEx.PasswordValidation, ErrorMessage = "Passwords must be at least 6 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; } = null!;

        // TODO: What else should we know about an Admin 
    }
}