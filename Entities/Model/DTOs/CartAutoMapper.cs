using AutoMapper;
using System.Net.Sockets;

namespace OrderService.Entities.Model.DTOs
{
    public partial class CartAutoMapper: Profile
    {
        public CartAutoMapper()
        {
            CreateMap<Cart, CartAddDto>()
            .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.CustomerID))
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.CartCustomization, opt => opt.MapFrom(src => src.CartCustomization));
            
            CreateMap<CartAddDto, Cart>();

            CreateMap<Cart, CartUpdateDto>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartUpdateDto, Cart>();
        }
    }



}



