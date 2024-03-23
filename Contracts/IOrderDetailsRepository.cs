using OrderService.Entities.Model;

namespace OrderService.Contracts
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
    {

        /// <summary>
        /// Clear the customer order details
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(Guid orderId);
    }
}
