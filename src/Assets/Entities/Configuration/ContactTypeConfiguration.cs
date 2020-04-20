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
                    new ContactType { Id = 1, Name = "Person" },
                    new ContactType { Id = 2, Name = "Organisation" }
                });
        }
    }
}
