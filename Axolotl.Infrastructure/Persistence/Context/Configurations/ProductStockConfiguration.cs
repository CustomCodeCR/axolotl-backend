using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        // Table & key
        builder.ToTable("ProductStock");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("ProductStockId");

        // Columns
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.QuantityOnHand).IsRequired();
        builder.Property(x => x.QuantityReserved).IsRequired();
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.WarehouseId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Warehouses)
               .WithMany(w => w.ProductStocks)
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.ProductStocks)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}