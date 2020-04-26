using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");

            builder.HasKey(x => x.Id)
                .HasName("PK_Contact")
                .IsClustered();

            builder.HasAlternateKey(x => x.Guid)
                .HasName("AK_Contact_Guid");

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

            builder.HasOne(x => x.ContactType)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.ContactTypeId)
                .HasConstraintName("FK_Contact_ContactType");

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.TenantId)
                .HasConstraintName("FK_Contact_Tenant");

            builder.HasIndex(x => new { x.TenantId, x.Guid })
                .IsUnique()
                .HasName("IX_Contact_Tenant_Guid");
        }
    }
}
