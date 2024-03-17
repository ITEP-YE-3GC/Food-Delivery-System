using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;

namespace OrderService.Entities.Seeds
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasData(
                // Customer 
                new OrderStatus { StatusID = 1, Name = "Submitted" },
                // Restaurant
                new OrderStatus { StatusID = 2, Name = "Received" },
                // Courier / Driver
                new OrderStatus { StatusID = 3, Name = " Picked up" },
                new OrderStatus { StatusID = 4, Name = "Onway" },
                new OrderStatus { StatusID = 5, Name = "Delivered" }
            );
        }
    }
}
