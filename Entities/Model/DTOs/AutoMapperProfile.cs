using AutoMapper;

namespace OrderService.Entities.Model.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderAddDTO,Orders>();
        }
    }
}
