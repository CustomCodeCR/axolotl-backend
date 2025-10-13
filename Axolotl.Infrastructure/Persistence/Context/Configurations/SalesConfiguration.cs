using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class SalesConfiguration : IEntityTypeConfiguration<Sales>
{
    public void Configure(EntityTypeBuilder<Sales> builder)
    {
        // Table & key
        builder.ToTable("Sales");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("SaleId");

        // Columns
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.Status).HasColumnType("sale_status");
        builder.Property(x => x.SaleDate).IsRequired();
        builder.Property(x => x.Notes).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.WarehouseId);
        builder.HasIndex(x => x.SaleDate);

        // Relationships
        builder.HasOne(x => x.Customers)
               .WithMany(c => c.Sales)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouses)
               .WithMany(x => x.Sales)
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.SaleItems)
               .WithOne(i => i.Sales)
               .HasForeignKey(i => i.SaleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}