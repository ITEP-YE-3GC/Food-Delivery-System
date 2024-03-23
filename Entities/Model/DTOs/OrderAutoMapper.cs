using AutoMapper;
using System.Net.Sockets;

namespace OrderService.Entities.Model.DTOs
{
    public class OrderAutoMapper : Profile
    {
        public OrderAutoMapper()
        {
            CreateMap<OrderAddDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.OrderCustomization, opt => opt.MapFrom(src => src.OrderCustomization));

            CreateMap<OrderDetailsAddDto, OrderDetails>()
                .ForMember(dest => dest.OrderID, opt => opt.Ignore());

            CreateMap<OrderCustomizationAddDto, OrderCustomization>()
                .ForMember(dest => dest.OrderID, opt => opt.Ignore());

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.OrderCustomization, opt => opt.MapFrom(src => src.OrderCustomization));
        }
    }
}
