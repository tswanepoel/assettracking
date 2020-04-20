using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class AssetTypeEntityTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.ToTable("AssetType");

            builder.HasKey(x => x.Id)
                .HasName("PK_AssetType")
                .IsClustered();

            builder.HasAlternateKey(x => x.Name)
                .HasName("AK_AssetType_Name");
                
            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasData(
                new[]
                {
                    new AssetType { Id = 1, Name = "Computer" },
                    new AssetType { Id = 2, Name = "Monitor" },
                    new AssetType { Id = 3, Name = "Phone" }
                });
        }
    }
}
