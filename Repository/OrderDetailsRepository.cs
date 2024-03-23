﻿using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;

namespace OrderService.Repository
{
    
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        /// <summary>
        /// Clear the customer's order
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(Guid orderId)
        {
            // Retrieve the order items to be removed
            var orderItems = _applicationContext.OrderDetails
                                .Where(o => o.OrderID == orderId)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.OrderDetails.RemoveRange(orderItems);
        }
    }
}
