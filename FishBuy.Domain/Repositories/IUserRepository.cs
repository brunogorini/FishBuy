using FishBuy.Domain.Entities;

namespace FishBuy.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
