using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;
using OrderService.Entities.Seeds;

namespace OrderService.Entities
{
    public class ApplicationContext: DbContext
    {

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            // Configure other entities and relationships if needed
        }

        public DbSet<User> users { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<OrderStatus> orderStatus { get; set; }
        public DbSet<Carts> carts { get; set; }
        public DbSet<OrderTracking> OrderTracking { get; set; }

        

    }
}
