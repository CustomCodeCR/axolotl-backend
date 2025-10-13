using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        // Table & key
        builder.ToTable("Products");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("ProductId");

        // Columns
        builder.Property(x => x.SKU).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.Name).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Description).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // FKs
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.UnitId).IsRequired();
        builder.Property(x => x.TaxRateId).IsRequired();
        builder.Property(x => x.SupplierId).IsRequired();

        // Indexes
        builder.HasIndex(x => x.SKU).IsUnique();
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.UnitId);
        builder.HasIndex(x => x.TaxRateId);
        builder.HasIndex(x => x.SupplierId);

        // Relationships
        builder.HasOne(x => x.Categories)
               .WithMany(c => c.Products)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Units)
               .WithMany()
               .HasForeignKey(x => x.UnitId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TaxRates)
               .WithMany()
               .HasForeignKey(x => x.TaxRateId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Suppliers)
               .WithMany()
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}