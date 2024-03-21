using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;
using System.Reflection.Emit;
using System;

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
