using OrderService.Entities.Model;
using static OrderService.Utilities.Constants;

namespace OrderService.Contracts
{
    
    public interface IOrdersRepository : IGenericRepository<Order>
    {
       
    }
}
