using Bogus;
using Ramble.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seeding.Ramble.Data
{
    public abstract class SeedingBase<TEntity> where TEntity : class
    {
        public Faker<TEntity> Generator { get; protected set; }

        protected readonly RambleDbContext DbContext;

        public SeedingBase(RambleDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
