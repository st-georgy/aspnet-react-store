using Microsoft.EntityFrameworkCore;
using aspnet_react_store.DataAccess.Entities;
using aspnet_react_store.DataAccess.Configurations;
using aspnet_react_store.DataAccess.Entities.Enums;

namespace aspnet_react_store.DataAccess
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        public DbSet<CartEntity> Carts { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<UserInfoEntity> UserInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartEntity>().ToTable("Cart");
            modelBuilder.Entity<OrderEntity>().ToTable("Order");
            modelBuilder.Entity<ProductEntity>().ToTable("Product");
            modelBuilder.Entity<UserEntity>().ToTable("User");
            modelBuilder.Entity<UserInfoEntity>().ToTable("UserInfo");

            modelBuilder.HasPostgresEnum<AccountTypeEnum>();
            modelBuilder.HasPostgresEnum<OrderStatusEnum>();

            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());

            LoadInitialData(modelBuilder);
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

        private static void LoadInitialData(ModelBuilder modelBuilder)
        {
            var users = new[] {
                new UserEntity { Id = 1, UserName = "admin", AccountType = AccountTypeEnum.Admin, PasswordHash = "63a9f0ea7bb98050796b649e85481845" },
                new UserEntity { Id = 2, UserName = "support", AccountType = AccountTypeEnum.Support, PasswordHash = "434990c8a25d2be94863561ae98bd682" },
                new UserEntity { Id = 3, UserName = "user", PasswordHash = "ee11cbb19052e40b07aac0ca060c23ee" },
            };
            modelBuilder.Entity<UserEntity>().HasData(users);

            var userInfos = new[] {
                new UserInfoEntity { Id = 1, UserId = 1 },
                new UserInfoEntity { Id = 2, UserId = 2 },
                new UserInfoEntity { Id = 3, UserId = 3 },
            };
            modelBuilder.Entity<UserInfoEntity>().HasData(userInfos);

            var carts = new[] {
                new CartEntity { Id = 1, UserId = 1 },
                new CartEntity { Id = 2, UserId = 2 },
                new CartEntity { Id = 3, UserId = 3 },
            };
            modelBuilder.Entity<CartEntity>().HasData(carts);

            // var products = new[] {
            //     new Product { Id = 1, Name = "T-Shirt", Price = 2300},
            //     new Product { Id = 2, Name = "Jeans", Price = 4900},
            //     new Product { Id = 3, Name = "Pants", Price = 5100},
            //     new Product { Id = 4, Name = "Socks", Price = 1300},
            // };

            var products = new List<ProductEntity>();
            var random = new Random();
            var product_first_names = new string[] {
                "Cotton", "Wool", "Silk", "Linen", "Bamboo", "Leather"
            };
            var product_second_names = new string[] {
                "Pants", "Trousers", "Socks", "Jeans", "T-Shirt", "Hoodie"
            };

            for (var i = 1; i < 100; i++)
            {
                var productName = product_first_names[random.Next(0, product_first_names.Length)] + " " +
                                    product_second_names[random.Next(0, product_second_names.Length)];
                var product = new ProductEntity { Id = i, Name = productName, Price = random.Next(1000, 10000) };
                products.Add(product);
            }
            modelBuilder.Entity<ProductEntity>().HasData(products);
        }
    }
}