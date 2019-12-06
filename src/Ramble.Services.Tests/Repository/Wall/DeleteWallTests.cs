using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Ramble.Data;
using Ramble.Data.Models;
using Ramble.Services.Repository.Wall;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ramble.Services.Tests.Repository.Wall
{
    public class DeleteWallTests
    {
        private readonly RambleDbContext _dbContext;

        public DeleteWallTests()
        {
            _dbContext = new RambleDbContext(new DbContextOptionsBuilder<RambleDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            _dbContext.Walls.Add(new WallEntity
            {
                Id = 1,
                Name = "Test"
            });

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task DeleteWall_FoundEntity_Succesful()
        {
            var handler = new DeleteWallHandler(_dbContext, NullLogger<DeleteWallHandler>.Instance);
            var result = await handler.Handle(new DeleteWall(1), default);

            Assert.True(result.IsSuccess);
            Assert.Empty(await _dbContext.Walls.ToListAsync());
        }

        [Fact]
        public async Task DeleteWall_NotFoundEntity_Error()
        {
            var handler = new DeleteWallHandler(_dbContext, NullLogger<DeleteWallHandler>.Instance);
            var result = await handler.Handle(new DeleteWall(2), default);

            Assert.True(result.IsError);
            Assert.NotEmpty(await _dbContext.Walls.ToListAsync());
        }
    }
}
