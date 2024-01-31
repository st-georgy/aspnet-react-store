using aspnet_react_store.DataAccess.Entities;
using aspnet_react_store.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
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