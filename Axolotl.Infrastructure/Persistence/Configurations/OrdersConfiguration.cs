using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> b)
    {
        b.ToTable("Orders");
        b.HasKey(x => x.UUID);

        b.Property(x => x.Notes).HasMaxLength(512);

        b.HasOne(x => x.Customers).WithMany().HasForeignKey(x => x.CustomerId);
        b.HasOne(x => x.Warehouses).WithMany().HasForeignKey(x => x.WarehouseId);

        b.HasMany(x => x.OrderItems)
         .WithOne(x => x.Orders)
         .HasForeignKey(x => x.OrderId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
{
    public void Configure(EntityTypeBuilder<OrderItems> b)
    {
        b.ToTable("OrderItems");
        b.HasKey(x => x.UUID);

        b.Property(x => x.UnitPrice).HasPrecision(18, 2);
        b.Property(x => x.DiscountPct).HasPrecision(5, 2).HasDefaultValue(0);
    }
}
