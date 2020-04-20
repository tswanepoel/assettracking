using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ContactPictureEntityTypeConfiguration : IEntityTypeConfiguration<ContactPicture>
    {
        public void Configure(EntityTypeBuilder<ContactPicture> builder)
        {
            builder.ToTable("ContactPicture");

            builder.HasKey(x => new { x.ContactId, x.PictureId })
                .HasName("PK_ContactPicture")
                .IsClustered();

            builder.Property(x => x.CreatedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ModifiedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.DeletedUser)
                .HasMaxLength(128);

            builder.HasOne(x => x.Contact)
                .WithMany(x => x.Pictures)
                .HasForeignKey(x => x.ContactId)
                .HasConstraintName("FK_ContactPicture_Contact");

            builder.HasOne(x => x.Picture)
                .WithMany()
                .HasForeignKey(x => x.ContactId)
                .HasConstraintName("FK_ContactPicture_Blob");
        }
    }
}
