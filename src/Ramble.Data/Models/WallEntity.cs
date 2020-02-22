using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Ramble.Data.Models
{
    public class WallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }

        public RambleUserEntity Creator { get; set; } = null!;
        public List<WallEntryEntity> WallEntries { get; set; } = null!;

        public class EntityConfiguration : IEntityTypeConfiguration<WallEntity>
        {
            public void Configure(EntityTypeBuilder<WallEntity> builder)
            {
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Name)
                    .HasMaxLength(56)
                    .IsRequired();

                builder.HasOne(e => e.Creator)
                    .WithMany(e => e.CreatedWalls)
                    .HasForeignKey(e => e.CreatedBy)
                    .IsRequired();
            }
        }
    }
}
