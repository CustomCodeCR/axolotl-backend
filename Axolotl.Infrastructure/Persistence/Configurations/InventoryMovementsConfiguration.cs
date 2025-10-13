using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Configurations;

public class InventoryMovementsConfiguration : IEntityTypeConfiguration<InventoryMovements>
{
    public void Configure(EntityTypeBuilder<InventoryMovements> b)
    {
        b.ToTable("InventoryMovements");
        b.HasKey(x => x.UUID);

        b.Property(x => x.ReferenceTable).HasMaxLength(64).IsRequired();
        b.Property(x => x.ReferenceId).HasMaxLength(64).IsRequired();
        b.Property(x => x.Reason).HasMaxLength(256);

        b.HasOne(x => x.Warehouses).WithMany().HasForeignKey(x => x.WarehouseId);
        b.HasOne(x => x.Products).WithMany().HasForeignKey(x => x.ProductId);
    }
}
