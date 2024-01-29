using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Server.Entities;
using aspnet_react_store.Server.DataLayer.Entities.Configurations;
using aspnet_react_store.Server.Entities.Enums;

namespace aspnet_react_store.Server
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserInfo> UserInfos { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.HasPostgresEnum<AccountTypeEnum>();
            modelBuilder.HasPostgresEnum<OrderStatusEnum>();

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());

            LoadInitialData(modelBuilder);
        }

        public override int SaveChanges()
        {
            // When User Entity added to Database - create UserInfo and Cart
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UserInfo = new UserInfo();
                    entry.Entity.Cart = new Cart();
                }
            }

            return base.SaveChanges();
        }

        private static void LoadInitialData(ModelBuilder modelBuilder)
        {
            var users = new[] {
                new User { Id = 1, UserName = "admin", AccountType = AccountTypeEnum.Admin, PasswordHash = "63a9f0ea7bb98050796b649e85481845" },
                new User { Id = 2, UserName = "support", AccountType = AccountTypeEnum.Support, PasswordHash = "434990c8a25d2be94863561ae98bd682" },
                new User { Id = 3, UserName = "user", PasswordHash = "ee11cbb19052e40b07aac0ca060c23ee" },
            };
            modelBuilder.Entity<User>().HasData(users);

            var userInfos = new[] {
                new UserInfo { Id = 1, UserId = 1 },
                new UserInfo { Id = 2, UserId = 2 },
                new UserInfo { Id = 3, UserId = 3 },
            };
            modelBuilder.Entity<UserInfo>().HasData(userInfos);

            var carts = new[] {
                new Cart { Id = 1, UserId = 1 },
                new Cart { Id = 2, UserId = 2 },
                new Cart { Id = 3, UserId = 3 },
            };
            modelBuilder.Entity<Cart>().HasData(carts);

            // var products = new[] {
            //     new Product { Id = 1, Name = "T-Shirt", Price = 2300},
            //     new Product { Id = 2, Name = "Jeans", Price = 4900},
            //     new Product { Id = 3, Name = "Pants", Price = 5100},
            //     new Product { Id = 4, Name = "Socks", Price = 1300},
            // };

            var products = new List<Product>();
            var random = new Random();
            var product_first_names = new string[] {
                "Cotton", "Wool", "Silk", "Linen", "Bamboo", "Leather"
            };
            var product_second_names = new string[] {
                "Pants", "Trousers", "Socks", "Jeans", "T-Shirt", "Hoodie"
            };

            for (var i = 1; i < 100; i++)
            {
                var productName = product_first_names[random.Next(0, product_first_names.Length)] +
                                    product_second_names[random.Next(0, product_second_names.Length)];
                var product = new Product { Id = i, Name = productName, Price = random.Next(1000, 10000) };
                products.Add(product);
            }
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}