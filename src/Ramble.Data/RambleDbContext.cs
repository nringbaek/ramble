using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ramble.Data.Models;
using System;

namespace Ramble.Data
{
    public class RambleDbContext : IdentityDbContext<RambleUserEntity, RambleUserRoleEntity, string>
    {
        public DbSet<JourneyEntryEntity> JourneyEntries { get; set; }

        public RambleDbContext(DbContextOptions<RambleDbContext> options) : base(options)
        {

        }
    }
}
