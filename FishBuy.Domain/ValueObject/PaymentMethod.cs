using FishBuy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Domain.ObjetoDeValor
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDebitCard 
        { 
            get { return Id == (int)PaymentMethodEnum.DebitCard; } 
        }

        public bool IsCreditCard
        {
            get { return Id == (int)PaymentMethodEnum.CreditCard; }
        }
        public bool IsPaymentService
        {
            get { return Id == (int)PaymentMethodEnum.PaymentService; }
        }
        public bool IsUndefined
        {
            get { return Id == (int)PaymentMethodEnum.Undefined; }
        }
    }
}
