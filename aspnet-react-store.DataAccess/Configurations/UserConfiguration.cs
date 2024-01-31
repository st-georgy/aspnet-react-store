using aspnet_react_store.DataAccess.Entities;
using aspnet_react_store.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
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