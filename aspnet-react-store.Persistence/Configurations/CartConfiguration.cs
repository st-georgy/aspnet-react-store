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

            builder.HasMany(c => c.Products)
                .WithMany(p => p.Carts)
                .UsingEntity(e => e.ToTable("ProductCart"));

            builder.HasOne(c => c.User)
                .WithOne(c => c.Cart);
        }
    }
}