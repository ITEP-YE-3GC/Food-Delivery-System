
namespace OrderService.Repository
{
    
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        public bool OrderExists(Guid orderId)
        {
            return _applicationContext.Orders.Find(orderId) != null;
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
