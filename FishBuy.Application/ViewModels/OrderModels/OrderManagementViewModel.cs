using FishBuy.Application.ViewModels.OrderItemModels;
using FishBuy.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FishBuy.Application.ViewModels.OrderModels
{
    public class OrderManagementViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public StatusEnum Status { get; set; }

        public int AddressNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public string PaymentMethod { get; set; }
        public float Total { get; set; }

        public ICollection<OrderItemViewModel> OrderItems { get; set; }
    }
}