using System.Text.RegularExpressions;

namespace FishBuy.Application.ViewModels.UserModels
{
    public class AuthenticateViewModel : Validation
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                AddMessage("Email was not informed");
            }
            var emailIsValid = Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!emailIsValid)
            {
                AddMessage("Email address is invalid");
            }
            if (string.IsNullOrEmpty(Password))
            {
                AddMessage("Password was not informed");
            }
        }
    }


}
