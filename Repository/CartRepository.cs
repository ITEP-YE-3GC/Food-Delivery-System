using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;
using static OrderService.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        public bool CartItemExists(Guid cartId)
        {
            return FindCartItem(cartId) != null;
        }

        public bool CartItemExists(long customerId, int productId)
        {
            return FindCartItem(customerId, productId) != null;
        }

        public Cart FindCartItem(Guid CartId)
        {
            var item = _applicationContext.Carts
                            .Find(CartId);

            return item;
        }

        public Cart FindCartItem(long customerId, int productId)
        {
            var item = _applicationContext.Carts
                        .FirstOrDefault(c => c.CustomerID == customerId && c.ProductID == productId);

            return item;
        }

        /// <summary>
        /// Clear the customer's cart
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(long customerId)
        {
            // Retrieve the cart items to be removed
            var cartItems = _applicationContext.Carts
                                .Where(c => c.CustomerID == customerId)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.Carts.RemoveRange(cartItems);
        }
    }
}
