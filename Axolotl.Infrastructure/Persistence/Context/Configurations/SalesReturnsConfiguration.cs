using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SalesReturnsConfiguration : IEntityTypeConfiguration<SalesReturns>
{
    public void Configure(EntityTypeBuilder<SalesReturns> builder)
    {
        // Table & key
        builder.ToTable("SalesReturns");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SalesReturnId");

        // Columns
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.ReturnDate).IsRequired();
        builder.Property(x => x.Reason).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => x.ReturnDate);

        // Relationships
        builder.HasOne(x => x.Invoices)
               .WithMany(i => i.SalesReturns)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}