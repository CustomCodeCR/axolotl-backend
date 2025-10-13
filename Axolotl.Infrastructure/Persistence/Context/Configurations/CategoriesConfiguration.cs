using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        // Table & key
        builder.ToTable("Categories");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("CategoryId");

        // Columns
        builder.Property(x => x.Name).HasMaxLength(150).IsUnicode(false);
        builder.Property(x => x.Description).IsUnicode(false);

        // Enum
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Name).IsUnique();
    }
}