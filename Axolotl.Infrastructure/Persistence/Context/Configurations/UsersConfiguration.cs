using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        // Table & key
        builder.ToTable("Users");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("UserId");

        // Columns
        builder.Property(x => x.UserName).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.Email).HasMaxLength(255).IsUnicode(false);
        builder.Property(x => x.Password).HasMaxLength(255).IsUnicode(false);
        builder.Property(x => x.EmployeeId).IsRequired();
        builder.Property(x => x.LastLoginAt).IsRequired();
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        // Relationships
        builder.HasOne(x => x.Employees)
               .WithMany(e => e.Users)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}