using Microsoft.EntityFrameworkCore;
using Ramble.Data;
using Seeding.Ramble.Data.Models;
using System;
using System.Threading.Tasks;

namespace Seeding.Ramble
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var dbContext = new RambleDbContext(new DbContextOptionsBuilder<RambleDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            var wallSeeding = new WallSeeding(dbContext);
            await wallSeeding.Seed(5);

            Console.WriteLine("Seeding done.");
        }
    }
}
