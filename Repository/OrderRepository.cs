
namespace OrderService.Repository
{
    
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
