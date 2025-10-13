using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Configurations;

public class CartsConfiguration : IEntityTypeConfiguration<Carts>
{
    public void Configure(EntityTypeBuilder<Carts> b)
    {
        b.ToTable("Carts");
        b.HasKey(x => x.UUID);
        b.HasIndex(x => x.CustomerId).IsUnique(false);

        b.HasMany(x => x.CartItems)
         .WithOne(x => x.Carts)
         .HasForeignKey(x => x.CartId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CartItemsConfiguration : IEntityTypeConfiguration<CartItems>
{
    public void Configure(EntityTypeBuilder<CartItems> b)
    {
        b.ToTable("CartItems");
        b.HasKey(x => x.UUID);
        b.Property(x => x.UnitPrice).HasPrecision(18, 2);
        b.HasOne(x => x.Products).WithMany().HasForeignKey(x => x.ProductId);
    }
}
