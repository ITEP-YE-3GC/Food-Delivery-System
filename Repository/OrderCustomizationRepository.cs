namespace OrderService.Repository
{
    public class OrderCustomizationRepository : GenericRepository<OrderCustomization>, IOrderCustomizationRepository
    {
        public OrderCustomizationRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }

        /// <summary>
        /// Clear the customer's order
        /// </summary>
        /// <param name="id">Customer ID</param>
        public void DeleteAll(Guid orderId)
        {
            // Retrieve the order items to be removed
            var orderItems = _applicationContext.OrderCustomizations
                                .Where(o => o.OrderID == orderId)
                                .ToList();

            // Remove the retrieved items
            _applicationContext.OrderCustomizations.RemoveRange(orderItems);
        }
    }
}
