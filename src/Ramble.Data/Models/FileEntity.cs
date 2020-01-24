using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Ramble.Data.Models
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string Filename { get; set; } = null!;
        public Guid FileLocationId { get; set; }

        public DateTimeOffset Created { get; set; }

        public class EntityConfiguration : IEntityTypeConfiguration<FileEntity>
        {
            public void Configure(EntityTypeBuilder<FileEntity> builder)
            {
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Filename)
                    .HasMaxLength(256)
                    .IsRequired();
            }
        }
    }
}
