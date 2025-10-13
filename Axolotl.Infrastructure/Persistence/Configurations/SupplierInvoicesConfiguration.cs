using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Configurations;

public class SupplierInvoicesConfiguration : IEntityTypeConfiguration<SupplierInvoices>
{
    public void Configure(EntityTypeBuilder<SupplierInvoices> b)
    {
        b.ToTable("SupplierInvoices");
        b.HasKey(x => x.UUID);

        b.Property(x => x.InvoiceNumber).HasMaxLength(50).IsRequired();
        b.Property(x => x.TotalExTax).HasPrecision(18, 2);
        b.Property(x => x.TotalTax).HasPrecision(18, 2);
        b.Property(x => x.TotalIncTax).HasPrecision(18, 2);

        b.HasOne(x => x.Suppliers).WithMany(x => x.SupplierInvoices).HasForeignKey(x => x.SupplierId);
        b.HasOne(x => x.PurchaseOrders).WithMany(x => x.SupplierInvoices).HasForeignKey(x => x.PurchaseOrderId);

        b.HasMany(x => x.SupplierInvoiceItems)
         .WithOne(x => x.SupplierInvoices)
         .HasForeignKey(x => x.SupplierInvoiceId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SupplierInvoiceItemsConfiguration : IEntityTypeConfiguration<SupplierInvoiceItems>
{
    public void Configure(EntityTypeBuilder<SupplierInvoiceItems> b)
    {
        b.ToTable("SupplierInvoiceItems");
        b.HasKey(x => x.UUID);
        b.Property(x => x.UnitCost).HasPrecision(18, 2);
        b.Property(x => x.DiscountPct).HasPrecision(5, 2);
        b.HasOne(x => x.Products).WithMany().HasForeignKey(x => x.ProductId);
    }
}
