using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(x => x.Price, propBuilder =>
        {
            propBuilder.Property(y => y.Currency).HasMaxLength(3);
            propBuilder.Property(y => y.Amount).HasPrecision(4, 2);
        });

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
