using OrderService.Entities.Model;
using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Carts> Carts { get; }
        IUserRepository User { get; }
        IOrdersRepository Order { get; }

        IOrderDetailsRepository OrderDetails { get; }
        // add more 
        int Complete();
    }
}
