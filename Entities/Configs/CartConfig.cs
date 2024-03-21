using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;

namespace OrderService.Entities.Configs
{
<<<<<<<< HEAD:Entities/Configs/CartsConfiguration.cs
    public class CartsConfiguration : IEntityTypeConfiguration<Cart>
========
    public class CartConfig: IEntityTypeConfiguration<Cart>
>>>>>>>> bd1ee69 (FOOD-6 #time 1d #done):Entities/Configs/CartConfig.cs
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
