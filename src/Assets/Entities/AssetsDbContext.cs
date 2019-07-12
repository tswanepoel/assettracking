using Microsoft.EntityFrameworkCore;
using Assets.Entities.Configurations;

namespace Assets.Entities
{
    public class AssetsDbContext : DbContext
    {
        public AssetsDbContext(DbContextOptions<AssetsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AssetAllocationChangeEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssetCommentEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssetEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssetPictureEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssetTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new BlobEntityTypeConfiguration());
            builder.ApplyConfiguration(new ComputerEntityTypeConfiguration());
            builder.ApplyConfiguration(new ContactCommentEntityTypeConfiguration());
            builder.ApplyConfiguration(new ContactEntityTypeConfiguration());
            builder.ApplyConfiguration(new ContactPictureEntityTypeConfiguration());
            builder.ApplyConfiguration(new ContactTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new PhoneEntityTypeConfiguration());
            builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new ScreenEntityTypeConfiguration());
            builder.ApplyConfiguration(new TenantEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserTenantRoleEntityTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
