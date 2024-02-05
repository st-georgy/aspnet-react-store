using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence.Mapping
{
    public static class Mapper
    {
        public static Cart Map(CartEntity entity) =>
            Cart.Create
            (
                entity.Id
            ).Value ?? throw new Exception();

        public static CartEntity Map(Cart domain) =>
            new()
            {
                Id = domain.Id,
            };

        public static Image Map(ImageEntity entity) =>
            Image.Create
            (
                entity.Id,
                entity.FilePath
            ).Value ?? throw new Exception();

        public static ImageEntity Map(Image domain) =>
            new()
            {
                Id = domain.Id,
                FilePath = domain.FilePath,
            };

        public static Order Map(OrderEntity entity) =>
            Order.Create
            (
                entity.Id,
                Map(entity.User!),
                (OrderStatus)entity.OrderStatus
            ).Value ?? throw new Exception();

        public static OrderEntity Map(Order domain) =>
            new()
            {
                Id = domain.Id,
                User = Map(domain.User),
                UserId = domain.User.Id,
                OrderStatus = (OrderStatusEnum)domain.OrderStatus,
            };

        public static Product Map(ProductEntity entity) =>
            Product.Create
            (
                entity.Id,
                entity.Name,
                entity.Price,
                entity.Description,
                entity.Images.Select(Map).ToList()
            ).Value ?? throw new Exception();

        public static ProductEntity Map(Product domain) =>
            new()
            {
                Id = domain.Id,
                Name = domain.Name,
                Price = domain.Price,
                Description = domain.Description,
                Images = domain.Images!.Select(Map).ToList(),
            };

        public static User Map(UserEntity entity) =>
            User.Create
            (
                entity.Id,
                entity.UserName,
                entity.Email,
                entity.PasswordHash,
                Map(entity.Cart!),
                Map(entity.UserInfo!),
                (AccountType)entity.AccountType
            ).Value ?? throw new Exception();

        public static UserEntity Map(User domain) =>
            new()
            {
                Id = domain.Id,
                UserName = domain.UserName,
                Email = domain.Email,
                PasswordHash = domain.PasswordHash,
                Cart = Map(domain.Cart!),
                UserInfo = Map(domain.UserInfo!),
                AccountType = (AccountTypeEnum)domain.AccountType,
            };

        public static UserInfo Map(UserInfoEntity entity) =>
            UserInfo.Create
            (
                entity.Id,
                entity.FirstName,
                entity.MiddleName,
                entity.LastName,
                entity.JoinDate
            ).Value ?? throw new Exception();

        public static UserInfoEntity Map(UserInfo domain) =>
            new()
            {
                Id = domain.Id,
                FirstName = domain.FirstName,
                MiddleName = domain.MiddleName,
                LastName = domain.LastName,
                JoinDate = domain.JoinDate,
            };
    }
}