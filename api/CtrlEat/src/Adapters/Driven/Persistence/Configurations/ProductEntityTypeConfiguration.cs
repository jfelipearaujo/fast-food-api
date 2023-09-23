using Domain.Entities;
using Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(y => y.Currency)
                .HasConversion(
                    currency => currency.Value,
                    value => new Currency(value))
                .HasMaxLength(3);

            builder.Property(y => y.Amount)
                .HasConversion(
                    amount => amount.Value,
                    value => new CurrencyAmount(value))
                .HasPrecision(4, 2);

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired()
                .HasPrecision(7);

            builder.Property(x => x.UpdatedAtUtc)
                .IsRequired()
                .HasPrecision(7);

            // Relationships

            builder.HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
