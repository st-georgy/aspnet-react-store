using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice)
                .HasPrecision(100, 2)
                .HasDefaultValueSql("0");

            builder.Property(o => o.TotalProducts)
                .HasDefaultValueSql("0");

            builder.Property(o => o.Discount)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0");

            builder.Property(o => o.OrderStatus)
                .HasDefaultValue(OrderStatusEnum.Pending);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
        }
    }
}