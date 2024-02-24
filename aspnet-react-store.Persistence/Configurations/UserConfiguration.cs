using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(u => u.UserName);

            builder.Property(u => u.Email)
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(32);

            builder.Property(u => u.UserName)
                .HasMaxLength(30);

            builder.HasMany(u => u.Products)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductFavorite",
                    r => r.HasOne<ProductEntity>()
                        .WithMany()
                        .HasForeignKey("ProductId"),
                    l => l.HasOne<UserEntity>()
                        .WithMany()
                        .HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "ProductId");
                        j.ToTable("ProductFavorite");
                    });

            builder.Property(u => u.UserRole)
                .HasDefaultValue(UserRoleEnum.User);
        }
    }
}