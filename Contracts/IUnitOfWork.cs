using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IOrdersRepository Order { get; }

        IOrderDetailsRepository OrderDetails { get; }

        ICartRepository Cart { get; }

        //object Carts { get; }

        // add more 
        int Complete();
    }
}
