using Domain.Entities;
using Domain.Entities.TypedIds;

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

            builder.Property(x => x.Id).HasConversion(
                productId => productId.Value,
                value => new ProductId(value));

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.OwnsOne(x => x.Price, priceBuilder =>
            {
                priceBuilder.Property(y => y.Currency).HasMaxLength(3);
            });

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
