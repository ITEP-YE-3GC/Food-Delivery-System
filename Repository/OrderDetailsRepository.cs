
namespace OrderService.Repository
{
    
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        public IEnumerable<OrderDetails> FindOrderItems(Guid orderId)
        {
            var items = _applicationContext.OrderDetails
                            .Where(o => o.OrderID == orderId).ToList();

            return items;
        }

        public OrderDetails FindOrderItem(Guid orderId, int productId)
        {
            var item = _applicationContext.OrderDetails
                        .FirstOrDefault(o => o.OrderID == orderId && o.ProductID == productId);

            return item;
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
