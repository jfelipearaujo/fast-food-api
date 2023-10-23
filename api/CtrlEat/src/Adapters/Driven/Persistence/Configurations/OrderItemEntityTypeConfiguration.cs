using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orders_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderItemId.Create(value));

        builder.Property(x => x.Quantity);

        builder.Property(x => x.Observation)
            .IsRequired(false)
            .HasMaxLength(250);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(x => x.Price, propBuilder =>
        {
            propBuilder.Property(y => y.Currency).HasMaxLength(3);
            propBuilder.Property(y => y.Amount).HasPrecision(7, 2);
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

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(s => s.ProductId);
    }
}
