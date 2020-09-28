using FishBuy.Domain.Entities;
using System.Linq;

namespace FishBuy.Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetAllOrders();
        IQueryable<Order> GetOrdersByUserId(int userId);
        Order GetLastOrder(int userId);
    }
}
