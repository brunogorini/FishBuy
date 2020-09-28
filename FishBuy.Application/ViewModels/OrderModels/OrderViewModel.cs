using FishBuy.Domain.Entities;
using FishBuy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Application.ViewModels.OrderModels
{
    public class OrderViewModel : Validation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public StatusEnum Status { get; set; }

        public int AddressNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public int PaymentMethodId { get; set; }
        public float Total { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public override void Validate()
        {
            ClearValidationMessage();

            if (!OrderItems.Any())
            {
                AddMessage("Order shoud have one order item or more");
            }
            if (string.IsNullOrEmpty(PostalCode))
            {
                AddMessage("PostalCode is required");
            }
            if (PaymentMethodId == 0)
            {
                AddMessage("Payment method is required");
            }
        }
    }
}
