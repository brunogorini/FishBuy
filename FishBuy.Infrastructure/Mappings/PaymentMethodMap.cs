using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FishBuy.Domain.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Infrastructure.Mappings
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
