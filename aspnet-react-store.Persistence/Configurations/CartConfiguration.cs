using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.ToTable("Cart");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.UserId, "Cart_UserId_key")
                .IsUnique();

            builder.Property(c => c.Discount)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0");

            builder.Property(c => c.TotalPrice)
                .HasPrecision(100, 2)
                .HasDefaultValueSql("0");

            builder.Property(c => c.TotalProducts)
                .HasDefaultValueSql("0");

            builder.HasOne(c => c.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<CartEntity>(c => c.UserId);
        }
    }
}