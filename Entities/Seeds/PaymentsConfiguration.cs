using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;

namespace OrderService.Entities.Seeds
{
   
    public class PaymentsConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.HasData(
                new Payments { PaymentID = 1, Name = "COD" }
            );
        }
    }
}
