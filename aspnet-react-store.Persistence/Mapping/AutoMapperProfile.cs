using AutoMapper;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;
using aspnet_react_store.Domain.Models.Linking;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cart, CartEntity>().ReverseMap();
            CreateMap<Category, CategoryEntity>().ReverseMap();
            CreateMap<Image, ImageEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<UserInfo, UserInfoEntity>().ReverseMap();
            CreateMap<ProductCart, ProductCartEntity>().ReverseMap();
            CreateMap<ProductOrder, ProductOrderEntity>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusEnum>().ReverseMap();
            CreateMap<UserRole, UserRoleEnum>().ReverseMap();
        }
    }
}
