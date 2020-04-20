using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant");

            builder.HasKey(x => x.Id)
                .HasName("PK_Tenant")
                .IsClustered();

            builder.HasAlternateKey(x => x.Area)
                .HasName("AK_Tenant_Area");

            builder.HasAlternateKey(x => x.Name)
                .HasName("AK_Tenant_Name");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Version)
                .IsRequired()
                .IsRowVersion();

            builder.Property(x => x.CreatedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ModifiedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.DeletedUser)
                .HasMaxLength(128);
        }
    }
}
