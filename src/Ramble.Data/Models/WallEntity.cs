using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Data.Models
{
    public class WallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<WallEntryEntity> WallEntries { get; set; }

        public class EntityConfiguration : IEntityTypeConfiguration<WallEntity>
        {
            public void Configure(EntityTypeBuilder<WallEntity> builder)
            {
                builder.HasKey(e => e.Id);
            }
        }
    }
}
