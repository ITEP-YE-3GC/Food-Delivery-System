
namespace OrderService.Contracts
{
    
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// Check if the order exists
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool OrderExists(Guid orderId);
    }
}
