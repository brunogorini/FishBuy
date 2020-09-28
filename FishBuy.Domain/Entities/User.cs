using System.Collections.Generic;

namespace FishBuy.Domain.Entities
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Administrator { get; set; }
        public string Role { get { return Administrator ? "Admin" : "User"; } }

        public virtual ICollection<Order> Orders { get; set; }


    }

}
