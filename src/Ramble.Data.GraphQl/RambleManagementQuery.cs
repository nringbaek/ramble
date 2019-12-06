using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Ramble.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramble.Data.GraphQl
{
    public class RambleManagementQuery
    {
        public Task<List<WallEntity>> GetWalls([Service]RambleDbContext dbContext)
        {
            return dbContext.Walls
                .Include(e => e.WallEntries)
                .ToListAsync();
        }

        public Task<WallEntity> GetWall([Service]RambleDbContext dbContext, int id)
        {
            return dbContext.Walls
                .Include(e => e.WallEntries)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
