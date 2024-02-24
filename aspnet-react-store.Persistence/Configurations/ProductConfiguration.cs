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

            builder.Property(p => p.Description)
                .HasMaxLength(1024);

            builder.Property(p => p.Discount)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0");

            builder.Property(p => p.Name)
                .HasMaxLength(50);

            builder.Property(p => p.Price)
                .HasPrecision(100, 2);

            builder.ToTable(t => t.HasCheckConstraint("ValidPrice", "\"Price\" > 0"));
        }
    }
}
