using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Configurations.Linking
{
    public class ProductCartEntityConfiguration
        : IEntityTypeConfiguration<ProductCartEntity>
    {
        public void Configure(EntityTypeBuilder<ProductCartEntity> builder)
        {
            builder.ToTable("ProductCart");

            builder.HasKey(po => new { po.ProductId, po.CartId });

            builder.Property(po => po.Quantity)
                .HasDefaultValueSql("0");

            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(po => po.ProductId);

            builder.HasOne(po => po.Cart)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(po => po.CartId);
        }
    }
}