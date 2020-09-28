using FishBuy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishBuy.Infrastructure.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.OrderDate)
                .IsRequired();

            builder.Property(p => p.DeliveryDate)
                .IsRequired();

            builder.Property(p => p.Status)
                 .IsRequired();

            builder.Property(p => p.AddressNumber)
                .IsRequired();

            builder.Property(p => p.Address)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(p => p.State)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(p => p.City)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(p => p.PostalCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.Total)
                 .IsRequired();


            builder.HasOne(p => p.PaymentMethod);
        }
    }
}
