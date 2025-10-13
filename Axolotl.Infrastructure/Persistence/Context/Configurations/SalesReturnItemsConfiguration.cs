using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SalesReturnItemsConfiguration : IEntityTypeConfiguration<SalesReturnItems>
{
    public void Configure(EntityTypeBuilder<SalesReturnItems> builder)
    {
        // Table & key
        builder.ToTable("SalesReturnItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SalesReturnItemId");

        // Columns
        builder.Property(x => x.SalesReturnId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(14, 4);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.SalesReturnId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.SalesReturns)
               .WithMany(sr => sr.SalesReturnItems)
               .HasForeignKey(x => x.SalesReturnId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.ReturnItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}