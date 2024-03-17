using AutoMapper;
using System.Net.Sockets;

namespace OrderService.Entities.Model.DTOs
{
    public partial class CartAutoMapper: Profile
    {
        public CartAutoMapper()
        {
            CreateMap<Cart, CartDTO>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartDTO, Cart>();
        }
    }
}
