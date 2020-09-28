using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FishBuy.Domain.Entities;

namespace FishBuy.Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(19,4)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}
