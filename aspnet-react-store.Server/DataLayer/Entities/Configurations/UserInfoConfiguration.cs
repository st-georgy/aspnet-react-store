using aspnet_react_store.Server.Entities;
using aspnet_react_store.Server.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspnet_react_store.Server.DataLayer.Entities.Configurations {
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo> {
        public void Configure(EntityTypeBuilder<UserInfo> builder) {
            builder.Property(i => i.JoinDate)
                    .HasDefaultValueSql("now()");
        }
    }
}