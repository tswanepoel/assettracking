using Microsoft.EntityFrameworkCore;
using Assets.Entities.Configuration;

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
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AssetConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
