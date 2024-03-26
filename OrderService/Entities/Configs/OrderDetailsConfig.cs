namespace OrderService.Entities.Configs
{
    public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
    {
        /// <summary>
        /// Create a composit key for [OrderID, ProductID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            //builder.HasNoKey();

            builder.HasKey(od => new { od.OrderID, od.ProductID });
        }
    }
}
