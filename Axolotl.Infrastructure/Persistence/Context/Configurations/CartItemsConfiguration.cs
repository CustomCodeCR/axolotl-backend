using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class CartItemsConfiguration : IEntityTypeConfiguration<CartItems>
{
    public void Configure(EntityTypeBuilder<CartItems> builder)
    {
        // Table & key
        builder.ToTable("CartItems");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("CartItemId");

        // Columns
        builder.Property(x => x.CartId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(14, 4);

        // Base enum
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Carts)
               .WithMany(c => c.CartItems)
               .HasForeignKey(x => x.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Products)
               .WithMany(p => p.CartItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}