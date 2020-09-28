using FishBuy.Domain.Entities;
using FishBuy.Domain.Repositories;
using FishBuy.Infrastructure.Context;
using System.Linq;

namespace FishBuy.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly FishBuyContext _fishBuyContext;
        public UserRepository(FishBuyContext fishBuyContext) : base(fishBuyContext)
        {
            _fishBuyContext = fishBuyContext;
        }

        public User GetUserByEmail(string email)
        {
            return _fishBuyContext.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
