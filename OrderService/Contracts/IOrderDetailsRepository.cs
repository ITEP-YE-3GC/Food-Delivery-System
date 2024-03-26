
namespace OrderService.Contracts
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
    {
        IEnumerable<OrderDetails> FindOrderItems(Guid orderId);
        OrderDetails FindOrderItem(Guid orderId, int productId);
        /// <summary>
        /// Clear the customer order details
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(Guid orderId);
    }
}
