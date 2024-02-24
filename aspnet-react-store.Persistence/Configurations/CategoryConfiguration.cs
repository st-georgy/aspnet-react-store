using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(50);

            builder.HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<ProductEntity>()
                        .WithMany()
                        .HasForeignKey("ProductId"),
                    l => l.HasOne<CategoryEntity>()
                        .WithMany()
                        .HasForeignKey("CategoryId"),
                    j =>
                    {
                        j.HasKey("CategoryId", "ProductId");
                        j.ToTable("ProductCategory");
                    });
        }

    }
}