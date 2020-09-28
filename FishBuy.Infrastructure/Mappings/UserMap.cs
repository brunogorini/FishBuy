using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FishBuy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(u => u.Orders)
                .WithOne(p => p.User);
        }
    }
}
