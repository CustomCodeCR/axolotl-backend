using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SupplierInvoiceItemsConfiguration : IEntityTypeConfiguration<SupplierInvoiceItems>
{
    public void Configure(EntityTypeBuilder<SupplierInvoiceItems> builder)
    {
        // Table & key
        builder.ToTable("SupplierInvoiceItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SupplierInvoiceItemId");

        // Columns
        builder.Property(x => x.SupplierInvoiceId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitCost).HasPrecision(14, 4);
        builder.Property(x => x.DiscountPct).HasPrecision(7, 3);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.SupplierInvoiceId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.SupplierInvoices)
               .WithMany(si => si.SupplierInvoiceItems)
               .HasForeignKey(x => x.SupplierInvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.SupplierInvoiceItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}