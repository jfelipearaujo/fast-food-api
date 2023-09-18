using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasPrecision(4, 2);

            builder
                .Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(x => x.CreatedAtUtc)
                .IsRequired()
                .HasPrecision(7);

            builder
                .Property(x => x.UpdatedAtUtc)
                .IsRequired()
                .HasPrecision(7);

            // Relationships

            builder
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
