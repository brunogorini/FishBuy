using AutoMapper;
using FishBuy.Application.ViewModels.OrderItemModels;
using FishBuy.Application.ViewModels.OrderModels;
using FishBuy.Application.ViewModels.ProductModels;
using FishBuy.Application.ViewModels.UserModels;
using FishBuy.Domain.Entities;

namespace FishBuy.Application.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UpdateViewModel>();
            CreateMap<User, UpdateViewModel>().ReverseMap();
            CreateMap<User, AuthenticateViewModel>();
            CreateMap<User, AuthenticateViewModel>().ReverseMap();
            CreateMap<User, AuthenticatedUserViewModel>();
            CreateMap<User, AuthenticatedUserViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, OrderManagementViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.Name));
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Product.FileName));
        }
    }
}
