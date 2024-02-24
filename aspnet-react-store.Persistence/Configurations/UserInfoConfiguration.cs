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

            builder.HasIndex(i => i.UserId, "UserInfo_UserId_key")
                .IsUnique();

            builder.Property(i => i.FirstName)
                .HasMaxLength(30);

            builder.Property(i => i.LastName)
                .HasMaxLength(30);

            builder.Property(i => i.MiddleName)
                .HasMaxLength(30);

            builder.Property(i => i.JoinDate)
                .HasDefaultValueSql("now()");

            builder.HasOne(i => i.User)
                .WithOne(u => u.UserInfo)
                .HasForeignKey<UserInfoEntity>(i => i.UserId);
        }
    }
}