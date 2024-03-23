using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;
using static OrderService.Utilities.Constants;

namespace OrderService.Repository
{
    
    public class OrdersRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrdersRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
