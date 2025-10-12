using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        // Table & key
        builder.ToTable("Roles");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("RoleId");

        // Columns
        builder.Property(x => x.Name).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.Description).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.IsSystem).IsRequired();
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Name).IsUnique();
    }
}