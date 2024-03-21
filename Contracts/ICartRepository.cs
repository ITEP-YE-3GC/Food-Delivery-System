using OrderService.Entities.Model;

namespace OrderService.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        /// <summary>
        /// Check if the item is aleady exists in cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        bool CartItemExists(Guid cartId);

        /// <summary>
        /// Check if the item is aleady exists in cart
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool CartItemExists(long customerId, int productId);

        /// <summary>
        /// Find cart item by Cart ID
        /// </summary>
        /// <param name="CartId"></param>
        /// <returns></returns>
        Cart FindCartItem(Guid CartId);

        /// <summary>
        /// Find cart item by Customer ID & Product ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Cart FindCartItem(long customerId, int productId);

        /// <summary>
        /// Clear the customer cart 
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(long customerId);
    }
}
