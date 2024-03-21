using OrderService.Entities;

namespace OrderService.Repository
{
    public class CartCustomizationRepository : GenericRepository<CartCustomization>, ICartCustomizationRepository
    {
        public CartCustomizationRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        /// <summary>
        /// Clear the customer's cart
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(Guid cartId)
        {
            // Retrieve the cart items to be removed
            var cartItems = _applicationContext.CartCustomizations
                                .Where(c => c.CartID == cartId)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.CartCustomizations.RemoveRange(cartItems);
        }
    }
}
