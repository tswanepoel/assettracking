using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(x => x.Id)
                .HasName("PK_Role")
                .IsClustered();

            builder.HasAlternateKey(x => x.Name)
                .HasName("AK_Role_Name");

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasData(
                new[]
                {
                    new ContactType { Id = 1, Name = "Administrator" },
                    new ContactType { Id = 2, Name = "Manager" },
                    new ContactType { Id = 3, Name = "Reader" }
                });
        }
    }
}
