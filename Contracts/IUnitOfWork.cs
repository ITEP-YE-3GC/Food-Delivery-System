using OrderService.Entities.Model;
using OrderService.Repository;

namespace OrderService.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IOrdersRepository Order { get; }
        IGenericRepository<Carts>Carts {  get; }

        IOrderDetailsRepository OrderDetails { get; }
        // add more 
        int Complete();
    }
}
