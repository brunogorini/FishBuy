using System.Text.RegularExpressions;

namespace FishBuy.Application.ViewModels.UserModels
{
    public class RegisterViewModel : Validation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Administrator { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                AddMessage("Email address was not informed");
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
            if (Password.Length < 6)
            {
                AddMessage("Password must be at least 6 characters");
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                AddMessage("First name was not informed");
            }

            if (string.IsNullOrEmpty(LastName))
            {
                AddMessage("Last name was not informed");
            }

        }
    }
}
