
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class MonitorEntityTypeConfiguration : IEntityTypeConfiguration<Monitor>
    {
        public void Configure(EntityTypeBuilder<Monitor> builder)
        {
            builder.ToTable("Monitor");

            builder.HasKey(x => x.MonitorId)
                .HasName("PK_Monitor")
                .IsClustered();

            builder.Property(x => x.SizeInches)
                .HasColumnType("DECIMAL(8, 1)");

            builder.HasOne(x => x.Asset)
                .WithOne(x => x.Monitor)
                .HasForeignKey<Monitor>(x => x.MonitorId)
                .HasConstraintName("FK_Monitor_Asset");
        }
    }
}
