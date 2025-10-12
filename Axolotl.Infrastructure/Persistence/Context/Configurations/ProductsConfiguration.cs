using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axolotl.Infrastructure.Persistence.Context.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> b)
        {
            
            b.ToTable("Products"); 

            b.HasKey(x => x.Id);

            b.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(x => x.Name)
                .HasMaxLength(200);

            b.Property(x => x.Active)
                .HasDefaultValue(true);

            b.HasIndex(x => x.Code)
                .IsUnique();

            // for this moment is commented because the Category entity is not created yet
            // b.HasOne(x => x.Category)
            //  .WithMany(c => c.Products)
            //  .HasForeignKey(x => x.CategoryId)
            //  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
