using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class PurchaseOrdersConfiguration : IEntityTypeConfiguration<PurchaseOrders>
{
    public void Configure(EntityTypeBuilder<PurchaseOrders> builder)
    {
        // Table & key
        builder.ToTable("PurchaseOrders");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("PurchaseOrderId");

        // Columns
        builder.Property(x => x.SupplierId).IsRequired();
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.Status).HasColumnType("purchase_status");
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.ExpectedDate).IsRequired();
        builder.Property(x => x.Notes).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.SupplierId);
        builder.HasIndex(x => x.WarehouseId);
        builder.HasIndex(x => x.OrderDate);

        // Relationships
        builder.HasOne(x => x.Suppliers)
               .WithMany(s => s.PurchaseOrders)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouses)
               .WithMany()
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.PurchaseOrderItems)
               .WithOne(i => i.PurchaseOrders)
               .HasForeignKey(i => i.PurchaseOrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SupplierInvoices)
               .WithOne(si => si.PurchaseOrders)
               .HasForeignKey(si => si.PurchaseOrderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}