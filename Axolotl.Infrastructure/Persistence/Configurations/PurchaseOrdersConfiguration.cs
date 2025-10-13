using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Configurations;

public class PurchaseOrdersConfiguration : IEntityTypeConfiguration<PurchaseOrders>
{
    public void Configure(EntityTypeBuilder<PurchaseOrders> b)
    {
        b.ToTable("PurchaseOrders");
        b.HasKey(x => x.UUID);
        b.Property(x => x.Notes).HasMaxLength(512);

        b.HasOne(x => x.Suppliers).WithMany(x => x.PurchaseOrders).HasForeignKey(x => x.SupplierId);
        b.HasOne(x => x.Warehouses).WithMany().HasForeignKey(x => x.WarehouseId);

        b.HasMany(x => x.PurchaseOrderItems)
         .WithOne(x => x.PurchaseOrders)
         .HasForeignKey(x => x.PurchaseOrderId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PurchaseOrderItemsConfiguration : IEntityTypeConfiguration<PurchaseOrderItems>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItems> b)
    {
        b.ToTable("PurchaseOrderItems");
        b.HasKey(x => x.UUID);
        b.Property(x => x.UnitCost).HasPrecision(18, 2);
        b.HasOne(x => x.Products).WithMany().HasForeignKey(x => x.ProductId);
    }
}
