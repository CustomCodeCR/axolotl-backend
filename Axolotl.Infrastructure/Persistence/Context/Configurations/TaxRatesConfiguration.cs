using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class TaxRatesConfiguration : IEntityTypeConfiguration<TaxRates>
{
    public void Configure(EntityTypeBuilder<TaxRates> builder)
    {
        // Table & key
        builder.ToTable("TaxRates");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("TaxRateId");

        // Columns
        builder.Property(x => x.Name).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.RatePercent).HasPrecision(7, 3);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Name).IsUnique();
    }
}