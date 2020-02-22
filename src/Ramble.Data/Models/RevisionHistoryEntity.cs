using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ramble.Data.Models.Types;
using System;

namespace Ramble.Data.Models
{
    public class RevisionHistoryEntity
    {
        public int Id { get; set; }
        public int EntryId { get; set; }

        public EntryType EntryType { get; set; }
        public string EntryContent { get; set; } = null!;
        public DateTimeOffset EntryTimestamp { get; set; }

        public string CreatedById { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }

        public RambleUserEntity CreatedBy { get; set; } = null!;

        public class EntityConfiguration : IEntityTypeConfiguration<RevisionHistoryEntity>
        {
            public void Configure(EntityTypeBuilder<RevisionHistoryEntity> builder)
            {
                builder.HasKey(e => e.Id);
                builder.HasIndex(e => e.EntryId);

                builder.HasOne(e => e.CreatedBy)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedById)
                    .IsRequired();
            }
        }
    }
}
