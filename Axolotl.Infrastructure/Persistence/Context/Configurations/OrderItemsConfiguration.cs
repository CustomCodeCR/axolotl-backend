using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
{
    public void Configure(EntityTypeBuilder<OrderItems> builder)
    {
        // Table & key
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("OrderItemId");

        // Columns
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(14, 4);
        builder.Property(x => x.DiscountPct).HasPrecision(7, 3);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.OrderId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Orders)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.OrderItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}