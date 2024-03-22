using AutoMapper;
using OrderService.Entities.Model.DTOs.RequestDTO;

namespace OrderService.Entities.Model.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<OrderAddDTO,Orders>();
            CreateMap<CartAddDTO,Carts>();

        }
    }
}
