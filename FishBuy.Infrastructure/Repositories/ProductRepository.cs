using FishBuy.Domain.Repositories;
using FishBuy.Domain.Entities;
using FishBuy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FishBuyContext fishBuyContext) : base(fishBuyContext)
        {
        }
    }
}
