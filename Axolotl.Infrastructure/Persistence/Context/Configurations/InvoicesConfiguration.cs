using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class InvoicesConfiguration : IEntityTypeConfiguration<Invoices>
{
    public void Configure(EntityTypeBuilder<Invoices> builder)
    {
        // Table & key
        builder.ToTable("Invoices");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("InvoiceId");

        // Columns
        builder.Property(x => x.Status).HasColumnType("invoice_status");
        builder.Property(x => x.InvoiceNumber).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.TotalExTax).HasPrecision(16, 4);
        builder.Property(x => x.TotalTax).HasPrecision(16, 4);
        builder.Property(x => x.TotalIncTax).HasPrecision(16, 4);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.InvoiceNumber).IsUnique();
        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.SaleId);
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.BillingAddressId);

        // Relationships
        builder.HasOne(x => x.Orders)
               .WithMany() // add collection on Orders if available
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Sales)
               .WithMany() // add collection on Sales if available
               .HasForeignKey(x => x.SaleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CustomerAddresses)
               .WithMany() // add collection on CustomerAddresses if available
               .HasForeignKey(x => x.BillingAddressId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}