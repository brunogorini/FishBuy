using AutoMapper;
using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.OrderModels;
using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using FishBuy.Domain.Enums;
using Sesc.IAD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Application.Services
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IMapper _mapper;

        public OrderAppService(IOrderBusiness orderBusiness, IMapper mapper)
        {
            _orderBusiness = orderBusiness;
            _mapper = mapper;
        }

        public ApplicationServiceResponse GetAllOrders()
        {
            var orders = _orderBusiness.GetAllOrders();
            var model = _mapper.Map<IEnumerable<OrderManagementViewModel>>(orders);
            return Ok(model.ToList());
        }

        public ApplicationServiceResponse GetOrdersByUserId(int userId)
        {
            var orders = _orderBusiness.GetOrdersByUserId(userId);
            var model = _mapper.Map<IEnumerable<OrderManagementViewModel>>(orders);
            return Ok(model.ToList());
        }

        public ApplicationServiceResponse PlaceOrder(OrderViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }
            var order = _mapper.Map<Order>(model);
            var placedOrderId = _orderBusiness.PlaceOrder(order);
            return Ok(placedOrderId);
        }

        public ApplicationServiceResponse UpdateOrderStatus(int orderId, StatusEnum orderStatus)
        {
            _orderBusiness.UpdateOrderStatus(orderId, orderStatus);
            return Ok();
        }
    }
}
