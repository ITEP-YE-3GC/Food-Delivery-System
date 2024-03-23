using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Model;
using System.Reflection.Emit;
using System;

namespace OrderService.Entities.Configs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Create a Index for [CustomerID, OrderDetailsID, OrderCustomization]
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(p => new { p.CustomerID, p.OrderDetails, p.OrderCustomization });
        }
    }
}
