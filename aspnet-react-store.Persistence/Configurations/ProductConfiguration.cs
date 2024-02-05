using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Carts)
                .WithMany(c => c.Products)
                .UsingEntity("ProductCart");

            builder.HasMany(p => p.Orders)
                .WithMany(o => o.Products)
                .UsingEntity("ProductOrder");

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("ValidPrice", "\"Price\" > 0"));
        }
    }
}
