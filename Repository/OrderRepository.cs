
namespace OrderService.Repository
{
    
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        public bool OrderItemExists(Guid orderId)
        {
            return FindOrderItem(orderId) != null;
        }

        public bool OrderItemExists(long customerId, int productId)
        {
            return FindOrderItem(customerId, productId) != null;
        }

        public Order FindOrderItem(Guid orderId)
        {
            var item = _applicationContext.Orders
                            .Find(orderId);

            return item;
        }

        public Order FindOrderItem(long customerId, int productId)
        {
            var item = _applicationContext.Orders
                        .FirstOrDefault(o => o.CustomerID == customerId && o.OrderDetails.Select( od => od.ProductID).Contains(productId));

            return item;
        }

        /// <summary>
        /// Clear the customer's cart
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(long customerId)
        {
            // Retrieve the order items to be removed
            var orderItems = _applicationContext.Orders
                                .Where(o => o.CustomerID == customerId)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.Orders.RemoveRange(orderItems);
        }
    }
}
