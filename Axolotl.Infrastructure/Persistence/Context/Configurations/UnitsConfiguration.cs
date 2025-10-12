using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class UnitsConfiguration : IEntityTypeConfiguration<Units>
{
    public void Configure(EntityTypeBuilder<Units> builder)
    {
        // Table & key
        builder.ToTable("Units");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("UnitId");

        // Columns
        builder.Property(x => x.Code).HasMaxLength(16).IsUnicode(false);
        builder.Property(x => x.Name).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}