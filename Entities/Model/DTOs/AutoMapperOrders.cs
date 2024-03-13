using AutoMapper;

namespace OrderService.Entities.Model.DTOs
{
    public class AutoMapperOrders:Profile
    {
        public AutoMapperOrders()
        {
            CreateMap<OrderAddDTO,Orders>();
        }
    }
}
