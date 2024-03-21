using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IOrdersRepository Order { get; }

        IOrderDetailsRepository OrderDetails { get; }

        ICartRepository Cart { get; }
        ICartCustomizationRepository CartCustomization { get; }

        // add more 
        int Complete();

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
