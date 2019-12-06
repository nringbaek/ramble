using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Ramble.Data.Infrastructure.Mssql
{
    public class RambleDbDesignFactory : IDesignTimeDbContextFactory<RambleDbContext>
    {
        public RambleDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            var connectionString = config.GetConnectionString("RambleDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<RambleDbContext>();

            Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString, dbOptions =>
                dbOptions.MigrationsAssembly(typeof(RambleDbDesignFactory).GetTypeInfo().Assembly.GetName().Name));

            return new RambleDbContext(optionsBuilder.Options);
        }
    }
}
