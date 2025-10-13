using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class InvoiceItemsConfiguration : IEntityTypeConfiguration<InvoiceItems>
{
    public void Configure(EntityTypeBuilder<InvoiceItems> builder)
    {
        // Table & key
        builder.ToTable("InvoiceItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("InvoiceItemId");

        // Columns
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(14, 4);
        builder.Property(x => x.DiscountPct).HasPrecision(7, 3);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.InvoiceId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Invoices)
               .WithMany(i => i.InvoiceItems)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.InvoiceItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}