using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class BlobEntityTypeConfiguration : IEntityTypeConfiguration<Blob>
    {
        public void Configure(EntityTypeBuilder<Blob> builder)
        {
            builder.ToTable("Blob");
            builder.HasKey(x => x.Id).HasName("PK_Blob").IsClustered();
            builder.HasAlternateKey(x => x.Guid).HasName("AK_Blob_Guid");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ContentType).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Content).IsRequired();
        }
    }
}
