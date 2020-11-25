using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configuration
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Asset");

            builder.HasKey(x => x.Id)
                .HasName("PK_Asset")
                .IsClustered();

            builder.HasAlternateKey(x => x.Guid)
                .HasName("AK_Asset_Guid");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Version)
                .IsRequired()
                .IsRowVersion();

            builder.Property(x => x.Description)
                .HasMaxLength(1024);

            builder.Property(x => x.SerialNumber)
                .HasMaxLength(128);

            builder.Property(x => x.Make)
                .HasMaxLength(128);

            builder.Property(x => x.Model)
                .HasMaxLength(128);

            builder.Property(x => x.Tag)
                .HasMaxLength(128);

            builder.Property(x => x.CreatedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ModifiedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.DeletedUser)
                .HasMaxLength(128);

            builder.HasOne(x => x.AssetType)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.AssetTypeId)
                .HasConstraintName("FK_Asset_AssetType");

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asset_Tenant");

            builder
                .HasOne(x => x.AllocatedContact)
                .WithMany(x => x.Assets)
                .HasForeignKey(x => x.AllocatedContactId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Asset_AllocatedContact");
                
            builder.HasIndex(x => new { x.TenantId, x.Guid })
                .IsUnique()
                .HasDatabaseName("IX_Asset_Tenant_Guid");

            builder.HasIndex(x => new { x.TenantId, x.Tag })
                .HasDatabaseName("IX_Asset_Tenant_Tag");
        }
    }
}
