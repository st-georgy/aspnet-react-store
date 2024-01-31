using aspnet_react_store.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.DataAccess.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Products)
                    .WithMany(p => p.Carts)
                    .UsingEntity(e => e.ToTable("ProductCart"));

            builder.HasOne(c => c.User)
                    .WithOne(c => c.Cart);
        }
    }
}