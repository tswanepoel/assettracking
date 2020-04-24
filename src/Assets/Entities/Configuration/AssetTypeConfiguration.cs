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
                    new AssetType { Id = (int)AssetTypeId.Computer, Name = "Computer" },
                    new AssetType { Id = (int)AssetTypeId.Monitor, Name = "Monitor" },
                    new AssetType { Id = (int)AssetTypeId.Phone, Name = "Phone" }
                });
        }
    }
}
