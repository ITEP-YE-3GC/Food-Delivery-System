using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;
using static OrderService.Utilities.Constants;

namespace OrderService.Repository
{
    
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
