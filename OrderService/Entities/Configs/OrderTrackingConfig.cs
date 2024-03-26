namespace OrderService.Entities.Configs
{
    public class OrderTrackingConfig : IEntityTypeConfiguration<OrderTracking>
    {
        /// <summary>
        /// Create a composit key for [OrderID, OrderStatusID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<OrderTracking> builder)
        {
            //builder.HasNoKey();

            builder.HasKey(ot => new { ot.OrderID, ot.OrderStatusID });
        }
    }
}
