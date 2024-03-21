using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Entities.Configs
{
    public class CartCustomizationConfig : IEntityTypeConfiguration<CartCustomization>
    {
        /// <summary>
        /// Create a composit key for [SeqID, CustomizationID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<CartCustomization> builder)
        {
            //builder.HasNoKey();

            builder.HasKey(c => new { c.CartID, c.CustomizationID });
        }
    }
}
