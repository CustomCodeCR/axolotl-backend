using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
{
    public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)
    {
        // Table & key
        builder.ToTable("ProductPriceHistory");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("ProductPriceHistoryId");

        // Columns
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ValidFrom).IsRequired();
        builder.Property(x => x.ValidTo).IsRequired();
        builder.Property(x => x.Price).HasPrecision(14, 4);
        builder.Property(x => x.Cost).HasPrecision(14, 4);
        builder.Property(x => x.Note).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => new { x.ProductId, x.ValidFrom }).IsUnique();

        // Relationships
        builder.HasOne(x => x.Products)
               .WithMany() // add collection on Products if available
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}