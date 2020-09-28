using FishBuy.Domain.Entities;
using System.Collections.Generic;

namespace FishBuy.Domain.Contracts
{
    public interface IUserBusiness
    {
        string GenerateToken(User user);
        User AuthenticateUser(string email, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        List<string> CreateUser(User user, string password);
        void UpdateUser(User userParam, string password = null);
        void DeleteUser(int id);
    }
}
