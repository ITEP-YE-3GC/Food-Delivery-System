using AutoMapper;
using System.Net.Sockets;

namespace OrderService.Entities.Model.DTOs
{
    public partial class CartAutoMapper: Profile
    {
        public CartAutoMapper()
        {
            CreateMap<CartAddDto, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.CartCustomization, opt => opt.MapFrom(src => src.CartCustomization));

            CreateMap<CartCustomizationAddDto, CartCustomization>()
                .ForMember(dest => dest.CartID, opt => opt.Ignore()); // Ignore the CartID when mapping

            //CreateMap<Cart, CartAddDto>()
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<CartUpdateDto, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.CartCustomization, opt => opt.MapFrom(src => src.CartCustomization));

            //CreateMap<CartUpdateDto, Cart>();
        }
    }



}



