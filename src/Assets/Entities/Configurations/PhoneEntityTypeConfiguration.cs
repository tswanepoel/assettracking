
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class PhoneEntityTypeConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone");
            builder.HasKey(x => x.PhoneId).HasName("PK_Phone").ForSqlServerIsClustered();
            builder.Property(x => x.Imei).HasMaxLength(128);
            builder.Property(x => x.Processor).HasMaxLength(128);
            builder.HasOne(x => x.Asset).WithOne(x => x.Phone).HasForeignKey<Phone>(x => x.PhoneId).HasConstraintName("FK_Phone_Asset");
        }
    }
}
