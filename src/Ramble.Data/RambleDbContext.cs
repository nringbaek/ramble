using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ramble.Data.Models;

namespace Ramble.Data
{
    public class RambleDbContext : IdentityDbContext<RambleUserEntity, RambleUserRoleEntity, string>
    {
        public DbSet<WallEntity> Walls { get; set; } = null!;
        public DbSet<WallEntryEntity> WallEntries { get; set; } = null!;

        public DbSet<FileEntity> Files { get; set; } = null!;

        public RambleDbContext(DbContextOptions<RambleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(RambleDbContext).Assembly);
        }
    }
}
