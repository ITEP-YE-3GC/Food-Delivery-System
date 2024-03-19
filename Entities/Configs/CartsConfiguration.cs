using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;

namespace OrderService.Entities.Configs
{
    public class CartsConfiguration : IEntityTypeConfiguration<Cart>
    {
        /// <summary>
        /// Create a composite key for [CustomerID, ProductID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => new { c.CustomerID, c.ProductID });
        }
    }

}
