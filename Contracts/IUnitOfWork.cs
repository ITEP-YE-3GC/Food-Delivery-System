using OrderService.Entities.Model;
using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IOrdersRepository Order { get; }

        IOrderDetailsRepository OrderDetails { get; }

        ICartsRepository Carts { get; }

        //IGenericRepository<Carts> Carts { get; }
        // add more 
        int Complete();
    }
}
