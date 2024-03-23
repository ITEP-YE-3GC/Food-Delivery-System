
namespace OrderService.Contracts
{
    
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// Check if the item is aleady exists in order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool OrderItemExists(Guid orderId);

        /// <summary>
        /// Check if the item is aleady exists in order
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool OrderItemExists(long customerId, int productId);

        /// <summary>
        /// Find order item by Order ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order FindOrderItem(Guid orderId);

        /// <summary>
        /// Find order item by Customer ID & Product ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Order FindOrderItem(long customerId, int productId);

        /// <summary>
        /// Clear the customer order 
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(long customerId);
    }
}
