using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ramble.Data.Models;
using System;

namespace Ramble.Data
{
    public class RambleDbContext : IdentityDbContext<RambleUserEntity, RambleUserRoleEntity, string>
    {
        public DbSet<WallEntity> Walls { get; set; } 
        public DbSet<WallEntryEntity> WallEntries { get; set; }

        public DbSet<FileEntity> Files { get; set; }

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
