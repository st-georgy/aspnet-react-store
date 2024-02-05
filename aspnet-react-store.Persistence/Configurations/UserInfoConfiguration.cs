using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Configurations
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfoEntity>
    {
        public void Configure(EntityTypeBuilder<UserInfoEntity> builder)
        {
            builder.ToTable("UserInfo");

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.User)
                .WithOne(u => u.UserInfo);

            builder.Property(i => i.JoinDate)
                .HasDefaultValueSql("now()");
        }
    }
}