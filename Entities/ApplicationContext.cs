﻿using Microsoft.EntityFrameworkCore;
using OrderService.Entities.Configs;
using OrderService.Entities.Model;
using OrderService.Entities.Seeds;

namespace OrderService.Entities
{
    public class ApplicationContext: DbContext
    {

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create index
            modelBuilder.ApplyConfiguration(new CartConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            // Add composite key for CartCustomization, OrderDetails, OrderCustomization:
            modelBuilder.ApplyConfiguration(new CartCustomizationConfig());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfig());
            modelBuilder.ApplyConfiguration(new OrderCustomizationConfig());

            // Seeds data:
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            // Configure other entities and relationships if needed
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderCustomization> OrderCustomizations { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrderTracking> OrderTracking { get; set; }

        // ============================
        // Change by: Anwar Hamzah
        // Change name to capital letter 
        // cart => Carts
        // ============================

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartCustomization> CartCustomizations { get; set; }


    }
}
