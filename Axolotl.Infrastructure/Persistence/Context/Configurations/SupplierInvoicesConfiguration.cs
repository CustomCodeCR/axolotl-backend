using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SupplierInvoicesConfiguration : IEntityTypeConfiguration<SupplierInvoices>
{
    public void Configure(EntityTypeBuilder<SupplierInvoices> builder)
    {
        // Table & key
        builder.ToTable("SupplierInvoices");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SupplierInvoiceId");

        // Columns
        builder.Property(x => x.PurchaseOrderId).IsRequired();
        builder.Property(x => x.SupplierId).IsRequired();
        builder.Property(x => x.InvoiceNumber).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.TotalExTax).HasPrecision(16, 4);
        builder.Property(x => x.TotalTax).HasPrecision(16, 4);
        builder.Property(x => x.TotalIncTax).HasPrecision(16, 4);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.PurchaseOrderId);
        builder.HasIndex(x => x.SupplierId);
        builder.HasIndex(x => new { x.SupplierId, x.InvoiceNumber }).IsUnique();

        // Relationships
        builder.HasOne(x => x.PurchaseOrders)
               .WithMany(x => x.SupplierInvoices)
               .HasForeignKey(x => x.PurchaseOrderId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Suppliers)
               .WithMany(x => x.SupplierInvoices)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}