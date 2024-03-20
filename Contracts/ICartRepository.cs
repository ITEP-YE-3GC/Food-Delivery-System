
namespace OrderService.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        /// <summary>
        /// Check if the item is aleady exists in cart
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool CartItemExists(int customerId, int productId);

        /// <summary>
        /// Find cart item by Customer ID & Product ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Cart FindCartItem(int customerId, int productId);

        /// <summary>
        /// Clear the customer cart 
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(int id);
    }
}
