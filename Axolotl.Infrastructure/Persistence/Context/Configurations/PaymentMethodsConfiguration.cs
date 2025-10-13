using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class PaymentMethodsConfiguration : IEntityTypeConfiguration<PaymentMethods>
{
    public void Configure(EntityTypeBuilder<PaymentMethods> builder)
    {
        // Table & key
        builder.ToTable("PaymentMethods");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("PaymentMethodId");

        // Columns
        builder.Property(x => x.Code).HasMaxLength(20).IsUnicode(false);
        builder.Property(x => x.Name).HasMaxLength(60).IsUnicode(false);
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.Code).IsUnique();
    }
}