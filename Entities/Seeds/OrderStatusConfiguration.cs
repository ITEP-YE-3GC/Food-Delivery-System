
namespace OrderService.Entities.Seeds
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasData(
                // Customer / Restaurant
                new OrderStatus { StatusID = 1, Name = "Cancelled" },
                ///// These statuses are exceptional, all other statuses can change to them, and they can change to any status.
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                new OrderStatus { StatusID = 2, Name = "Halted", Automated = true },
                new OrderStatus { StatusID = 3, Name = "Obstcles" },
                ////////////////////////////////////////////////////////////////////////////////////////////////
                new OrderStatus { StatusID = 4, Name = "Submitted", NextStep = 1 },
                // Restaurant
                new OrderStatus { StatusID = 5, Name = "Received", NextStep = 2 },
                new OrderStatus { StatusID = 6, Name = "Ready for Pick up", NextStep = 3 },
                // Courier / Driver
                new OrderStatus { StatusID = 7, Name = "Picked up", NextStep = 4 },
                new OrderStatus { StatusID = 8, Name = "On the way", NextStep = 5 },
                new OrderStatus { StatusID = 9, Name = "Delivered", NextStep = 6 }
            );
        }
    }
}
