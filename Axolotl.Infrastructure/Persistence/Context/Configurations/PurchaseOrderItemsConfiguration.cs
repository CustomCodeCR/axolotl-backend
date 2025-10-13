using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class PurchaseOrderItemsConfiguration : IEntityTypeConfiguration<PurchaseOrderItems>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItems> builder)
    {
        // Table & key
        builder.ToTable("PurchaseOrderItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("PurchaseOrderItemId");

        // Columns
        builder.Property(x => x.PurchaseOrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitCost).HasPrecision(14, 4);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.PurchaseOrderId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.PurchaseOrders)
               .WithMany(po => po.PurchaseOrderItems)
               .HasForeignKey(x => x.PurchaseOrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.PurchaseOrderItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}