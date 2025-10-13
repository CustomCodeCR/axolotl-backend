using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        // Table & key
        builder.ToTable("Orders");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("OrderId");

        // Columns
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.Status).HasColumnType("order_status");
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.RequiredDate).IsRequired();
        builder.Property(x => x.Notes).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.WarehouseId);

        // Relationships
        builder.HasOne(x => x.Customers)
               .WithMany(c => c.Orders)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouses)
               .WithMany()
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.OrderItems)
               .WithOne(i => i.Orders)
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}