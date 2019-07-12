using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class AssetCommentEntityTypeConfiguration : IEntityTypeConfiguration<AssetComment>
    {
        public void Configure(EntityTypeBuilder<AssetComment> builder)
        {
            builder.ToTable("AssetComment");
            builder.HasKey(x => x.Id).HasName("PK_AssetComment").ForSqlServerIsClustered();
            builder.HasAlternateKey(x => x.Guid).HasName("AK_AssetComment_Guid");
            builder.Property(x => x.Text).HasMaxLength(128).IsRequired();
            builder.Property(x => x.CreatedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.DeletedUser).HasMaxLength(128);
            builder.HasOne(x => x.Asset).WithMany(x => x.Comments).HasForeignKey(x => x.AssetId).HasConstraintName("FK_AssetComment_Asset");
        }
    }
}
