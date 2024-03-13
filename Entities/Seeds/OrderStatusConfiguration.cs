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
                new OrderStatus { StatusID = 1, Name = "Status 1" },
                new OrderStatus { StatusID = 2, Name = "Status 2" },
                new OrderStatus { StatusID = 3, Name = "Status 3" },
                new OrderStatus { StatusID = 4, Name = "Status 4" },
                new OrderStatus { StatusID = 5, Name = "Status 5" }
            );
        }
    }
}
