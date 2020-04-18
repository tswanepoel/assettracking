using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class UserTenantRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserTenantRole>
    {
        public void Configure(EntityTypeBuilder<UserTenantRole> builder)
        {
            builder.ToTable("UserTenantRole");
            builder.HasKey(x => new { x.UserId, x.TenantId, x.RoleId }).HasName("PK_UserTenantRole").IsClustered();
            builder.HasOne(x => x.User).WithMany(x => x.TenantRoles).HasForeignKey(x => x.UserId).HasConstraintName("FK_UserTenantRole_User");
            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.UserId).HasConstraintName("FK_UserTenantRole_Tenant");
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).HasConstraintName("FK_UserTenantRole_Role");
        }
    }
}
