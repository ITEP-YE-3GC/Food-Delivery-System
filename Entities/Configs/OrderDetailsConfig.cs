using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Entities.Configs
{
    public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
    {
        /// <summary>
        /// Create a composit key for [OrderID, SeqID]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            //builder.HasNoKey();

            builder.HasKey(o => new { o.OrderID, o.Seq });
        }
    }
}