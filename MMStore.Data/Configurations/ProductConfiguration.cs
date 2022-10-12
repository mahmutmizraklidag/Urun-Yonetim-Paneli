using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MMStore.Entities;

namespace MMStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
            builder.Property(x => x.ProductCode).HasMaxLength(30);
            builder.HasOne(b => b.Brand).WithMany(p => p.Products).HasForeignKey(b => b.BrandId); 
            builder.HasOne(b => b.Category).WithMany(p => p.Products).HasForeignKey(b => b.CategoryId);
        }
    }
}