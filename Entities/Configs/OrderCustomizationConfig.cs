using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Entities.Configs
{
    public class OrderCustomizationConfig : IEntityTypeConfiguration<OrderCustomization>
    {
        /// <summary>
        /// Create a composit key for [OrderID, CustomizationID, SeqID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<OrderCustomization> builder)
        {
            //builder.HasNoKey();

            builder.HasKey(o => new { o.OrderID, o.CustomizationID, o.Seq });
        }
    }
}