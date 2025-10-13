using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class CustomersConfiguration : IEntityTypeConfiguration<Customers>
{
    public void Configure(EntityTypeBuilder<Customers> builder)
    {
        // Table & key
        builder.ToTable("Customers");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("CustomerId");

        // Columns
        builder.Property(x => x.ID).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.FirstName).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.LastName).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Email).HasMaxLength(255).IsUnicode(false);
        builder.Property(x => x.Phone).HasMaxLength(30).IsUnicode(false);

        // Enums (PostgreSQL enum type must be registered in OnModelCreating)
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.ID).IsUnique();
    }
}