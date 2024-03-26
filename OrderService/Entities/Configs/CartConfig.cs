
namespace OrderService.Entities.Configs
{
    public class CartConfig: IEntityTypeConfiguration<Cart>
    {
        /// <summary>
        /// Create a Index for [CustomerID, ProductID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasIndex(p => new { p.CustomerID, p.ProductID });
        }
    }

}
