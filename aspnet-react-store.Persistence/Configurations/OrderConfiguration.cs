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

            builder.HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(e => e.ToTable("ProductOrder"));

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders);

            builder.Property(o => o.OrderStatus)
                .HasDefaultValue(OrderStatusEnum.Pending);
        }
    }
}