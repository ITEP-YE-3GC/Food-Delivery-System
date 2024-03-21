namespace OrderService.Contracts
{
    public interface ICartCustomizationRepository : IGenericRepository<CartCustomization>
    {

        /// <summary>
        /// Clear the customer cart customization
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(Guid cartId);
    }
}
