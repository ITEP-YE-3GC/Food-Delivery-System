using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IOrdersRepository Order { get; }

        IOrderDetailsRepository OrderDetails { get; }
        // add more 
        int Complete();
    }
}
