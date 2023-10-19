using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PaymentId.Create(value));

        builder.Property(x => x.Status);

        builder.OwnsOne(x => x.Price, propBuilder =>
        {
            propBuilder.Property(y => y.Currency).HasMaxLength(3);
            propBuilder.Property(y => y.Amount).HasPrecision(4, 2);
        });

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired()
            .HasPrecision(7);

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired()
            .HasPrecision(7);

        // Relationships

        builder.HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId);
    }
}