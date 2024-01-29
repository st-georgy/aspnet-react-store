using aspnet_react_store.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.Server.DataLayer.Entities.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasMany(c => c.Products)
                    .WithMany(p => p.Carts)
                    .UsingEntity(e => e.ToTable("ProductCart"));
        }
    }
}