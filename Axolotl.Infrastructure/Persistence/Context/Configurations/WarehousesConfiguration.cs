using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class WarehousesConfiguration : IEntityTypeConfiguration<Warehouses>
{
    public void Configure(EntityTypeBuilder<Warehouses> builder)
    {
        // Table & key
        builder.ToTable("Warehouses");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("WarehouseId");

        // Columns
        builder.Property(x => x.Code).HasMaxLength(30).IsUnicode(false);
        builder.Property(x => x.Name).HasMaxLength(150).IsUnicode(false);
        builder.Property(x => x.Address).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Province).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Canton).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Distric).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Country).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Code).IsUnique();
    }
} 