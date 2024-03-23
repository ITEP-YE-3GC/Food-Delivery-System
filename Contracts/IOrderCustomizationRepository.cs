namespace OrderService.Contracts
{
    public interface IOrderCustomizationRepository
    {

        /// <summary>
        /// Clear the customer order customization
        /// </summary>
        /// <param name="id">Customer ID</param>
        void DeleteAll(Guid orderId);
    }
}
