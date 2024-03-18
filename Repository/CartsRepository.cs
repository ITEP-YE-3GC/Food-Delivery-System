using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities;

namespace OrderService.Repository
{
   
    public class CartsRepository : GenericRepository<Carts>, ICartsRepository
    {
        public CartsRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
