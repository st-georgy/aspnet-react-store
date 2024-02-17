using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Persistence.Configurations;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        public DbSet<CartEntity> Carts { get; set; } = null!;
        public DbSet<ImageEntity> Images { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<UserInfoEntity> UserInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<UserRoleEnum>();
            modelBuilder.HasPostgresEnum<OrderStatusEnum>();

            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
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

        public async Task<int> GetNextSequenceValue(string sequenceName)
        {
            var sql = $"SELECT last_value FROM \"{sequenceName}\";";
            return await Database.ExecuteSqlRawAsync(sql) + 1;
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
                new UserEntity { Id = 1, UserName = "admin", Email = "admin@localhost", UserRole = UserRoleEnum.Admin, PasswordHash = "63a9f0ea7bb98050796b649e85481845" },
                new UserEntity { Id = 2, UserName = "support", Email = "support@localhost", UserRole = UserRoleEnum.Support, PasswordHash = "434990c8a25d2be94863561ae98bd682" },
                new UserEntity { Id = 3, UserName = "user", Email = "test@localhost", PasswordHash = "ee11cbb19052e40b07aac0ca060c23ee" },
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

            var products = new[] {
                new ProductEntity { Id = 1, Name = "T-Shirt", Price = 2300},
                new ProductEntity { Id = 2, Name = "Jeans", Price = 4900},
            };
            modelBuilder.Entity<ProductEntity>().HasData(products);

            var images = new[] {
                new ImageEntity { Id = 1, FilePath = "/Images/products/1/1.png", ProductId = 1 },
                new ImageEntity { Id = 2, FilePath = "/Images/products/1/2.png", ProductId = 1 },
            };
            modelBuilder.Entity<ImageEntity>().HasData(images);
        }
    }
}