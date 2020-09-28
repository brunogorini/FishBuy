using FishBuy.Domain.Entities;
using FishBuy.Domain.Enums;
using System.Collections.Generic;

namespace FishBuy.Domain.Contracts
{
    public interface IOrderBusiness
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(int userId);
        int PlaceOrder(Order order);
        void UpdateOrderStatus(int orderId, StatusEnum orderStatus);
    }
}
