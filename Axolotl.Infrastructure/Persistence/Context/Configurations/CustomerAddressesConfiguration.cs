using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Contexts.Configurations;

public class CustomerAddressesConfiguration : IEntityTypeConfiguration<CustomerAddresses>
{
    public void Configure(EntityTypeBuilder<CustomerAddresses> builder)
    {
        // Table & key
        builder.ToTable("CustomerAddresses");
        builder.HasKey(x => x.UUID);
        builder.Property(x => x.UUID).HasColumnName("CustomerAddressId");

        // Columns
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Label).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.Address1).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Address2).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.Province).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.Canton).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.District).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.PostalCode).HasMaxLength(20).IsUnicode(false);
        builder.Property(x => x.Country).HasMaxLength(100).IsUnicode(false);
        builder.Property(x => x.IsDefault).HasDefaultValue(false);

        // Enum
        builder.Property(x => x.State).HasColumnType("state_type");

        // Indexes
        builder.HasIndex(x => x.CustomerId);

        // Relationships
        builder.HasOne(x => x.Customers)
               .WithMany(c => c.CustomerAddresses)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}