using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class PaymentsConfiguration : IEntityTypeConfiguration<Payments>
{
    public void Configure(EntityTypeBuilder<Payments> builder)
    {
        // Table & key
        builder.ToTable("Payments");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("PaymentId");

        // Columns
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.PaymentMethodId).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(16, 4);
        builder.Property(x => x.PaymentDate).IsRequired();
        builder.Property(x => x.Reference).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Status).HasColumnType("payment_status");
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => x.PaymentMethodId);
        builder.HasIndex(x => x.Reference);

        // Relationships
        builder.HasOne(x => x.Invoices)
               .WithMany(i => i.Payments)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PaymentMethods)
               .WithMany(pm => pm.Payments)
               .HasForeignKey(x => x.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}