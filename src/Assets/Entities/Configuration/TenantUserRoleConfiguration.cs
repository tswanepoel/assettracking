using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class TenantUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<TenantUserRole>
    {
        public void Configure(EntityTypeBuilder<TenantUserRole> builder)
        {
            builder.ToTable("TenantUserRole");

            builder.HasKey(x => new { x.TenantId, x.UserId, x.RoleId })
                .HasName("PK_TenantUserRole")
                .IsClustered();

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.TenantId)
                .HasConstraintName("FK_TenantUserRole_Tenant");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_TenantUserRole_User");

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .HasConstraintName("FK_UserTenantRole_Role");
        }
    }
}
