using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));

        builder.Property(x => x.Status);

        builder.Property(y => y.TrackId)
            .HasConversion(
                trackId => trackId.Value,
                value => TrackId.Create(value))
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(x => x.StatusUpdatedAt)
            .IsRequired()
            .HasPrecision(7);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired()
            .HasPrecision(7);

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired()
            .HasPrecision(7);

        // Relationships

        builder.HasOne(o => o.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.ClientId);
    }
}
