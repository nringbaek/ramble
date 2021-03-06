﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ramble.Data.Models.Types;
using System;

namespace Ramble.Data.Models
{
    public class WallEntryEntity
    {
        public int Id { get; set; }
        public int WallId { get; set; }

        public EntryType EntryType { get; set; }
        public string EntryContent { get; set; } = null!;
        public DateTimeOffset EntryTimestamp { get; set; }

        public string CreatedById { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        
        public WallEntity Wall { get; set; } = null!;
        public RambleUserEntity CreatedBy { get; set; } = null!;

        public class EntityConfiguration : IEntityTypeConfiguration<WallEntryEntity>
        {
            public void Configure(EntityTypeBuilder<WallEntryEntity> builder)
            {
                builder.HasKey(e => e.Id);
                
                builder.HasOne(e => e.Wall)
                    .WithMany(e => e.WallEntries)
                    .HasForeignKey(e => e.WallId)
                    .IsRequired();

                builder.HasOne(e => e.CreatedBy)
                    .WithMany(e => e.CreatedWallEntries)
                    .HasForeignKey(e => e.CreatedById)
                    .IsRequired();
            }
        }
    }
}
