namespace FishBuy.Application.ViewModels.UserModels
{
    public class AuthenticatedUserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Administrator { get; set; }
        public string Token { get; set; }
    }
}
