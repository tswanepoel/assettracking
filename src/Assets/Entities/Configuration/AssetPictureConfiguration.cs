using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class AssetPictureEntityTypeConfiguration : IEntityTypeConfiguration<AssetPicture>
    {
        public void Configure(EntityTypeBuilder<AssetPicture> builder)
        {
            builder.ToTable("AssetPicture");

            builder.HasKey(x => new { x.AssetId, x.PictureId })
                .HasName("PK_AssetPicture")
                .IsClustered();

            builder.Property(x => x.CreatedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ModifiedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.DeletedUser)
                .HasMaxLength(128);

            builder.HasOne(x => x.Asset)
                .WithMany(x => x.Pictures)
                .HasForeignKey(x => x.AssetId)
                .HasConstraintName("FK_AssetPicture_Asset");

            builder.HasOne(x => x.Picture)
                .WithMany()
                .HasForeignKey(x => x.AssetId)
                .HasConstraintName("FK_AssetPicture_Blob");
        }
    }
}
