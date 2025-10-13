using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
{
    public void Configure(EntityTypeBuilder<Employees> builder)
    {
        // Table & key
        builder.ToTable("Employees");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("EmployeeId");

        // Columns
        builder.Property(x => x.ID).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.FirstName).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.LastName).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Email).HasMaxLength(255).IsUnicode(false);
        builder.Property(x => x.Phone).HasMaxLength(30).IsUnicode(false);
        builder.Property(x => x.HireDate).IsRequired();

        // Enum
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.ID).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}