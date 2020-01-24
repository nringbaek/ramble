using Microsoft.EntityFrameworkCore;
using Moq;
using Ramble.Common;
using Ramble.Common.Core;
using Ramble.Data;
using Ramble.Data.Models;
using Ramble.Services.Repository.Wall.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ramble.Services.Tests.Repository.Wall.Rules
{
    public class CanAdministerWallRuleTests : IDisposable
    {
        private readonly RambleDbContext _dbContext;

        public CanAdministerWallRuleTests()
        {
            _dbContext = new RambleDbContext(new DbContextOptionsBuilder<RambleDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            _dbContext.Walls.Add(new WallEntity
            {
                Id = 1,
                Name = "Test",
                CreatorId = Guid.NewGuid().ToString()
            });

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task WallNotFound_ReturnsFalse()
        {
            var requestContextMock = new Mock<IRequestContext>();
            var ruleEngine = new CanAdministerWallRuleEngine(_dbContext, requestContextMock.Object);
            var result = await ruleEngine.IsAuthorized(new CanAdministerWallRule { WallId = 2 });

            Assert.False(result);
        }

        [Fact]
        public async Task IdentityIsCreator_ReturnsTrue()
        {
            var requestContextMock = new Mock<IRequestContext>();
            var userId = await _dbContext.Walls.Select(e => e.CreatorId).FirstAsync();
            requestContextMock.Setup(e => e.Identity).Returns(RequestIdentity.Authenticated(userId, new List<string>()));

            var ruleEngine = new CanAdministerWallRuleEngine(_dbContext, requestContextMock.Object);
            var result = await ruleEngine.IsAuthorized(new CanAdministerWallRule { WallId = 1 });

            Assert.True(result);
        }

        [Fact]
        public async Task IdentityNotCreator_ReturnsFalse()
        {
            var requestContextMock = new Mock<IRequestContext>();
            requestContextMock.Setup(e => e.Identity).Returns(RequestIdentity.Authenticated(Guid.NewGuid().ToString(), new List<string>()));

            var ruleEngine = new CanAdministerWallRuleEngine(_dbContext, requestContextMock.Object);
            var result = await ruleEngine.IsAuthorized(new CanAdministerWallRule { WallId = 1 });

            Assert.False(result);
        }

        #region IDisposable support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
