using aspnet_react_store.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.DataAccess.Configurations
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfoEntity>
    {
        public void Configure(EntityTypeBuilder<UserInfoEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.User)
                    .WithOne(u => u.UserInfo);

            builder.Property(i => i.JoinDate)
                    .HasDefaultValueSql("now()");
        }
    }
}