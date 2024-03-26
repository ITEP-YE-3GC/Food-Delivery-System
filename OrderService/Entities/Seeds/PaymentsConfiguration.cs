
namespace OrderService.Entities.Seeds
{
   
    public class PaymentsConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(
                new Payment { PaymentID = 1, Name = "COD" }
            );
        }
    }
}
