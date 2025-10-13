using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItems>
{
    public void Configure(EntityTypeBuilder<SaleItems> builder)
    {
        // Table & key
        builder.ToTable("SaleItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SaleItemId");

        // Columns
        builder.Property(x => x.SaleId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(14, 4);
        builder.Property(x => x.DiscountPct).HasPrecision(7, 3);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.SaleId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Sales)
               .WithMany(s => s.SaleItems)
               .HasForeignKey(x => x.SaleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.SaleItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}