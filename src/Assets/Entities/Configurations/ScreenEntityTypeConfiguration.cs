
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class ScreenEntityTypeConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.ToTable("Screen");
            builder.HasKey(x => x.ScreenId).HasName("PK_Screen").ForSqlServerIsClustered();
            builder.Property(x => x.SizeInches).HasColumnType("DECIMAL(8, 1)");
            builder.HasOne(x => x.Asset).WithOne(x => x.Screen).HasForeignKey<Screen>(x => x.ScreenId).HasConstraintName("FK_Screen_Asset");
        }
    }
}
