using aspnet_react_store.Server.Entities;
using aspnet_react_store.Server.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.Server.DataLayer.Entities.Configurations {
    public class OrderConfiguration : IEntityTypeConfiguration<Order> {
        public void Configure(EntityTypeBuilder<Order> builder) {
            builder.Property(o => o.OrderStatus)
                    .HasConversion<string>()
                    .HasDefaultValue(OrderStatusEnum.Pending);

            builder.HasMany(o => o.Products)
                    .WithMany(p => p.Orders)
                    .UsingEntity(e => e.ToTable("ProductOrder"));
        }
    }
}