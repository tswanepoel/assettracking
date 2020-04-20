using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ContactTypeEntityTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.ToTable("ContactType");
            
            builder.HasKey(x => x.Id)
                .HasName("PK_ContactType")
                .IsClustered();

            builder.HasAlternateKey(x => x.Name)
                .HasName("AK_ContactType_Name");

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasData(
                new[]
                {
                    new ContactType { Id = (int)ContactTypeId.Person, Name = "Person" },
                    new ContactType { Id = (int)ContactTypeId.Organisation, Name = "Organisation" }
                });
        }
    }
}
