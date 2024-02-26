using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Configurations.Linking
{
    public class ProductOrderEntityConfiguration
        : IEntityTypeConfiguration<ProductOrderEntity>
    {
        public void Configure(EntityTypeBuilder<ProductOrderEntity> builder)
        {
            builder.ToTable("ProductOrder");

            builder.HasKey(po => new { po.ProductId, po.OrderId });

            builder.Property(po => po.Quantity)
                .HasDefaultValueSql("0");

            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId);

            builder.HasOne(po => po.Order)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.OrderId);
        }
    }
}