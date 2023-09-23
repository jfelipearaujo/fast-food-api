using Domain.Adapters.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("product");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(y => y.Amount)
                .HasPrecision(4, 2);

            builder
                .Property(y => y.Currency)
                .HasMaxLength(3);

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
