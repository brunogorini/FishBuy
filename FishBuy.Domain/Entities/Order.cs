using FishBuy.Domain.Enums;
using FishBuy.Domain.ObjetoDeValor;
using System;
using System.Collections.Generic;

namespace FishBuy.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public StatusEnum Status { get; set; }

        public int AddressNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }


        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public float Total { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
