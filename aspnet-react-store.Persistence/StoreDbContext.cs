using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Persistence.Configurations;
using aspnet_react_store.Persistence.Configurations.Linking;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        public DbSet<CartEntity> Carts { get; set; } = null!;
        public DbSet<CategoryEntity> Categories { get; set; } = null!;
        public DbSet<ImageEntity> Images { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<UserInfoEntity> UserInfos { get; set; } = null!;

        public DbSet<ProductCartEntity> ProductCarts { get; set; } = null!;
        public DbSet<ProductOrderEntity> ProductOrders { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<UserRoleEnum>();
            modelBuilder.HasPostgresEnum<OrderStatusEnum>();

            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());

            modelBuilder.ApplyConfiguration(new ProductCartEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOrderEntityConfiguration());
        }

        public override int SaveChanges()
        {
            // When User Entity added to Database - create UserInfo and Cart
            ProcessAddedUserEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // When User Entity added to Database - create UserInfo and Cart
            ProcessAddedUserEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessAddedUserEntities()
        {
            foreach (var entry in ChangeTracker.Entries<UserEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UserInfo = new UserInfoEntity();
                    entry.Entity.Cart = new CartEntity();
                }
            }
        }

        public async Task SeedData()
        {
            var categories = new[]
            {
                new CategoryEntity { Name = "T-Shirts" },
                new CategoryEntity { Name = "Pants" },
                new CategoryEntity { Name = "Oversize" }
            };
            await Categories.AddRangeAsync(categories);

            var users = new[] {
                new UserEntity { UserName = "admin", Email = "admin@localhost", UserRole = UserRoleEnum.Admin, PasswordHash = "63a9f0ea7bb98050796b649e85481845" },
                new UserEntity { UserName = "support", Email = "support@localhost", UserRole = UserRoleEnum.Support, PasswordHash = "434990c8a25d2be94863561ae98bd682" },
                new UserEntity { UserName = "user", Email = "test@localhost", PasswordHash = "ee11cbb19052e40b07aac0ca060c23ee" },
            };
            await Users.AddRangeAsync(users);

            var products = new[] {
                new ProductEntity { Name = "T-Shirt", Quantity = 5, Price = 2300, Categories = [categories[0], categories[2]],
                Description = "Aliquip nulla magna duis anim officia laborum adipisicing aliqua. Reprehenderit aute dolore adipisicing laboris magna enim voluptate labore dolore eu laborum culpa ut sunt. Aliqua cupidatat eiusmod" },
                new ProductEntity { Name = "Jeans", Quantity = 1, Discount = 0.5m, Price = 4900, Categories = [categories[1]],
                Description = "Aliquip nulla magna duis anim officia laborum adipisicing aliqua. Reprehenderit aute dolore adipisicing laboris magna enim voluptate labore dolore eu laborum culpa ut sunt. Aliqua cupidatat eiusmod" },
            };
            await Products.AddRangeAsync(products);

            var images = new[] {
                new ImageEntity { FilePath = "/Images/products/1/1.png", Product = products[0] },
                new ImageEntity { FilePath = "/Images/products/1/2.png", Product = products[0] },
            };
            await Images.AddRangeAsync(images);

            await SaveChangesAsync();
        }
    }
}