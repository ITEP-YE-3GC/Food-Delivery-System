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

        public Cart FindCartItem(int customerId, int productId)
        {
            var item = _applicationContext.Carts
                        .FirstOrDefault(c => c.CustomerID == customerId && c.ProductID == productId);

            return item;
        }

        /// <summary>
        /// Clear the customer's cart
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(int id)
        {
            // Retrieve the cart items to be removed
            var cartItems = _applicationContext.Carts
                                .Where(c => c.CustomerID == id)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.Carts.RemoveRange(cartItems);
        }
    }
}
