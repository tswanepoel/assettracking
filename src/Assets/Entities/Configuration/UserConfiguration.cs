using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assets.Entities.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id)
                .HasName("PK_User")
                .IsClustered();

            builder.HasAlternateKey(x => x.UserName)
                .HasName("AK_User_UserName");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Version)
                .IsRequired()
                .IsRowVersion();

            builder.Property(x => x.UserName)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(128);

            builder.Property(x => x.FirstName)
                .HasMaxLength(128);

            builder.Property(x => x.Surname)
                .HasMaxLength(128);

            builder.Property(x => x.Phone)
                .HasMaxLength(128);

            builder.Property(x => x.Email)
                .HasMaxLength(128);

            builder.Property(x => x.CreatedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.ModifiedUser)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.DeletedUser)
                .HasMaxLength(128);
        }
    }
}
