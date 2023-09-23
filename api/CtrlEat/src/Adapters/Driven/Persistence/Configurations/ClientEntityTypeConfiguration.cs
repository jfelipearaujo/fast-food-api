using Domain.Entities;
using Domain.Entities.StrongIds;
using Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                clientId => clientId.Value,
                value => ClientId.Create(value));

            builder.Property(y => y.FirstName)
                .HasConversion(
                    firstName => firstName.Value,
                    value => new Name(value))
                .HasMaxLength(250);

            builder.Property(y => y.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => new Name(value))
                .HasMaxLength(250);

            builder.Property(y => y.Email)
                .HasConversion(
                    email => email.Value,
                    value => new Email(value))
                .HasMaxLength(250);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.DocumentId)
                .HasConversion(
                    documentId => documentId.Value,
                    value => new DocumentId(value))
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
}
