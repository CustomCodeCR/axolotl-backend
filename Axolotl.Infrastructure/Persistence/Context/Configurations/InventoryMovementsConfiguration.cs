using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class InventoryMovementsConfiguration : IEntityTypeConfiguration<InventoryMovements>
{
    public void Configure(EntityTypeBuilder<InventoryMovements> builder)
    {
        // Table & key
        builder.ToTable("InventoryMovements");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("InventoryMovementId");

        // Columns
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.MovementType).HasColumnType("inventory_movement_type");
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.ReferenceTable).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.ReferenceId).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.Reason).IsUnicode(false);

        // Base enum
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.WarehouseId, x.ProductId });
        builder.HasIndex(x => new { x.ReferenceTable, x.ReferenceId });

        // Relationships
        builder.HasOne(x => x.Warehouses)
               .WithMany(w => w.InventoryMovements)
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.InventoryMovements)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}