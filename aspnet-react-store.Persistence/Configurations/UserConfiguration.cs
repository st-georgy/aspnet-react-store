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

            builder.HasOne(u => u.Cart)
                .WithOne(c => c.User);

            builder.HasOne(u => u.UserInfo)
                .WithOne(i => i.User);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User);

            builder.Property(u => u.UserName)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.AccountType)
                .HasDefaultValue(AccountTypeEnum.User);
        }
    }
}