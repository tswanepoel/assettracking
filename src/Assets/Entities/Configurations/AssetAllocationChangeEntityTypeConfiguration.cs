using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class AssetAllocationChangeEntityTypeConfiguration : IEntityTypeConfiguration<AssetAllocationChange>
    {
        public void Configure(EntityTypeBuilder<AssetAllocationChange> builder)
        {
            builder.ToTable("AssetAllocationChange");
            builder.HasKey(x => new { x.AssetId, x.ContactId }).HasName("PK_AssetAllocationChange").ForSqlServerIsClustered();
            builder.Property(x => x.CreatedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.DeletedUser).HasMaxLength(128);
            builder.HasOne(x => x.Asset).WithMany(x => x.AllocationChanges).HasForeignKey(x => x.AssetId).HasConstraintName("FK_AssetAllocationChange_Asset");
            builder.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId).HasConstraintName("FK_AssetAllocationChange_Contact");
        }
    }
}
