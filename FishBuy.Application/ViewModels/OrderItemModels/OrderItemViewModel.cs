using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Application.ViewModels.OrderItemModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
