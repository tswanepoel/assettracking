using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ComputerEntityTypeConfiguration : IEntityTypeConfiguration<Computer>
    {
        public void Configure(EntityTypeBuilder<Computer> builder)
        {
            builder.ToTable("Computer");
            builder.HasKey(x => x.ComputerId).HasName("PK_Computer").IsClustered();
            builder.Property(x => x.Processor).HasMaxLength(128);
            builder.HasOne(x => x.Asset).WithOne(x => x.Computer).HasForeignKey<Computer>(x => x.ComputerId).HasConstraintName("FK_Computer_Asset");
        }
    }
}
