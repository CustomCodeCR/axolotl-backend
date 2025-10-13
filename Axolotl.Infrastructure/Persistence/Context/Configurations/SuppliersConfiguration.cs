using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SuppliersConfiguration : IEntityTypeConfiguration<Suppliers>
{
    public void Configure(EntityTypeBuilder<Suppliers> builder)
    {
        // Table & key
        builder.ToTable("Suppliers");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SupplierId");

        // Columns
        builder.Property(x => x.ID).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.Name).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.ContactName).HasMaxLength(150).IsUnicode(false);
        builder.Property(x => x.Email).HasMaxLength(255).IsUnicode(false);
        builder.Property(x => x.Phone).HasMaxLength(30).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.ID).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Email);
    }
}