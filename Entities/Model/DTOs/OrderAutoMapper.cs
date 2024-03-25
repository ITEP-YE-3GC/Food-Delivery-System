
namespace OrderService.Entities.Model.DTOs
{
    public class OrderAutoMapper : Profile
    {
        public OrderAutoMapper()
        {
            CreateMap<OrderAddDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.OrderStatusID, opt => opt.MapFrom(src => src.StatusID))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            //CreateMap<OrderDetailsAddDto, OrderDetails>()
            //    .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID));

            CreateMap<OrderDetailsAddDto, OrderDetails>()
                .ForMember(dest => dest.OrderID, opt => opt.Ignore());

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.OrderStatusID, opt => opt.MapFrom(src => src.StatusID))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}
