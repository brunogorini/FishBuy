using FishBuy.Domain.Entities;
using FishBuy.Domain.ObjetoDeValor;
using FishBuy.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FishBuy.Infrastructure.Context
{
    public class FishBuyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public FishBuyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Classes de Mapeamento
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new PaymentMethodMap());

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod()
                {
                    Id = 1,
                    Name = "Debit Card",
                    Description = "Payment Method Debit Card"
                },
                new PaymentMethod()
                {
                    Id = 2,
                    Name = "Credit Card",
                    Description = "Payment Method Credit Card"
                },
                new PaymentMethod()
                {
                    Id = 3,
                    Name = "Payment Service",
                    Description = "Payment Method Payment Service"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }

}
