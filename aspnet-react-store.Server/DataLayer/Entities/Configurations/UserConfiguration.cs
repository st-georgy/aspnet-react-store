using aspnet_react_store.Server.Entities;
using aspnet_react_store.Server.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.Server.DataLayer.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.AccountType)
                    .HasDefaultValue(AccountTypeEnum.User);

            builder.HasAlternateKey(u => u.UserName);
        }
    }
}