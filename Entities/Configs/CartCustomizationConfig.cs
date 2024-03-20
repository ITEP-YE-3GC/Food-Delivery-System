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
            builder.HasAlternateKey(c => new { c.SeqID, c.CustomizationID });
        }
    }
}
