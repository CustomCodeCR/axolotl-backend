using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class CartsConfiguration : IEntityTypeConfiguration<Carts>
{
    public void Configure(EntityTypeBuilder<Carts> builder)
    {
        // Table & key
        builder.ToTable("Carts");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("CartId");

        // Columns
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Status)
               .HasColumnType("cart_status"); // PostgreSQL enum

        // Enum on base entity
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.CustomerId);

        // Relationships
        builder.HasOne(x => x.Customers)
               .WithMany(c => c.Carts)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}