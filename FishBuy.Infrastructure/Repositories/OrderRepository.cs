using FishBuy.Domain.Entities;
using FishBuy.Domain.Repositories;
using FishBuy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FishBuy.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly FishBuyContext _fishBuycontext;
        public OrderRepository(FishBuyContext fishBuycontext) : base(fishBuycontext)
        {
            _fishBuycontext = fishBuycontext;
        }

        public IQueryable<Order> GetAllOrders()
        {
            var result = _fishBuycontext.Orders
               .Include(x => x.OrderItems)
               .ThenInclude(x => x.Product)
               .Include(x => x.User)
               .Include(x => x.PaymentMethod)
               .OrderByDescending(x => x.OrderDate);

            return result;
        }

        public IQueryable<Order> GetOrdersByUserId(int userId)
        {
            var result = _fishBuycontext.Orders
              .Include(x => x.OrderItems)
              .ThenInclude(x => x.Product)
              .Include(x => x.PaymentMethod)
              .Where(x => x.UserId == userId)
              .OrderByDescending(x => x.OrderDate);

            return result;
        }

        public Order GetLastOrder(int userId)
        {
            var ordersList = _fishBuycontext.Orders
               .Where(o => o.UserId == userId).ToList();

            return ordersList.LastOrDefault();
        }


    }
}
