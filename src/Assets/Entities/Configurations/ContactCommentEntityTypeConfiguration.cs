using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ContactCommentEntityTypeConfiguration : IEntityTypeConfiguration<ContactComment>
    {
        public void Configure(EntityTypeBuilder<ContactComment> builder)
        {
            builder.ToTable("ContactComment");
            builder.HasKey(x => x.Id).HasName("PK_ContactComment").IsClustered();
            builder.HasAlternateKey(x => x.Guid).HasName("AK_ContactComment_Guid");
            builder.Property(x => x.Text).HasMaxLength(128).IsRequired();
            builder.Property(x => x.CreatedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedUser).HasMaxLength(128).IsRequired();
            builder.Property(x => x.DeletedUser).HasMaxLength(128);
            builder.HasOne(x => x.Contact).WithMany(x => x.Comments).HasForeignKey(x => x.ContactId).HasConstraintName("FK_ContactComment_Contact");
        }
    }
}
