using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Server.Entities;
using aspnet_react_store.Server.DataLayer.Entities.Configurations;
using Npgsql;
using aspnet_react_store.Server.Entities.Enums;

namespace aspnet_react_store.Server {
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options) {
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
        }
    }
}