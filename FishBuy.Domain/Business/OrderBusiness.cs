using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using FishBuy.Domain.Enums;
using FishBuy.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Domain.Business
{
    public class OrderBusiness : IOrderBusiness
    {

        private readonly IOrderRepository _orderRepository;

        public OrderBusiness(IOrderRepository orderRepository)

        {
            _orderRepository = orderRepository;

        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders().ToList();
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId).ToList();
        }

        public int PlaceOrder(Order order)
        {
            _orderRepository.Add(order);
            var placedOrder = _orderRepository.GetLastOrder(order.UserId);
            return placedOrder.Id;

        }

        public void UpdateOrderStatus(int orderId, StatusEnum orderStatus)
        {
            var order = _orderRepository.GetById(orderId);
            order.Status = orderStatus;
            _orderRepository.Update(order);
        }
    }

}
