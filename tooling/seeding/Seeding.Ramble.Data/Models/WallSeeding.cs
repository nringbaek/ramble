using Bogus;
using Ramble.Data;
using Ramble.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeding.Ramble.Data.Models
{
    public class WallSeeding : SeedingBase<WallEntity>
    {
        public WallSeeding(RambleDbContext dbContext) : base(dbContext)
        {
            Generator = new Faker<WallEntity>()
                .RuleFor(e => e.Name, (f, u) => f.Commerce.Product());
        }

        public Task Seed(int wallsToGenerate)
        {
            DbContext.Walls.AddRange(Generator.Generate(wallsToGenerate));
            return DbContext.SaveChangesAsync();
        }
    }
}
