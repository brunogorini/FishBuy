using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FishBuy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Infrastructure.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);
            
            builder.Property(i => i.ProductId)
                .IsRequired();

            builder.Property(i => i.Quantity)
                .IsRequired();
        }
    }
}
