using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ClientId.Create(value));

        builder.OwnsOne(x => x.FullName, propBuilder =>
        {
            propBuilder.Property(y => y.FirstName).HasMaxLength(250);
            propBuilder.Property(y => y.LastName).HasMaxLength(250);
        });

        builder.Property(y => y.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value)
            .HasMaxLength(250);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.DocumentId)
            .HasConversion(
                documentId => documentId.Value,
                value => DocumentId.Create(value).Value)
            .HasMaxLength(14);

        builder
            .HasIndex(x => x.DocumentId)
            .IsUnique();

        builder.Property(y => y.DocumentType);

        builder.Property(x => x.IsAnonymous);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired()
            .HasPrecision(7);

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired()
            .HasPrecision(7);
    }
}
