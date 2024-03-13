using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;

namespace OrderService.Repository
{
    
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
