using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Ramble.Data.Models
{
    public class WallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CreatorId { get; set; } = null!;

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
                    .WithMany(e => e.Walls)
                    .HasForeignKey(e => e.CreatorId)
                    .IsRequired();
            }
        }
    }
}
