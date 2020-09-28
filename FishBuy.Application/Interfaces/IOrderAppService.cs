using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.OrderModels;
using FishBuy.Domain.Enums;

namespace Sesc.IAD.Application.Interfaces
{
    public interface IOrderAppService
    {
        ApplicationServiceResponse GetAllOrders();
        ApplicationServiceResponse GetOrdersByUserId(int userId);
        ApplicationServiceResponse PlaceOrder(OrderViewModel model);
        ApplicationServiceResponse UpdateOrderStatus(int orderId, StatusEnum orderStatus);
    }
}
