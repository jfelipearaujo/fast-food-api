using Domain.Entities;
using Domain.Entities.TypedIds;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                clientId => clientId.Value,
                value => new ClientId(value));

            builder.OwnsOne(x => x.FullName, nameBuilder =>
            {
                nameBuilder
                    .Property(y => y.FirstName)
                    .HasMaxLength(250);

                nameBuilder
                    .Property(y => y.LastName)
                    .HasMaxLength(250);
            });

            builder.OwnsOne(x => x.Email, nameBuilder =>
            {
                nameBuilder
                    .Property(y => y.Address)
                    .HasMaxLength(250);

                nameBuilder
                    .HasIndex(x => x.Address)
                    .IsUnique();
            });

            builder.OwnsOne(x => x.PersonalDocument, personalDocumentBuilder =>
            {
                personalDocumentBuilder
                    .Property(y => y.DocumentId)
                    .HasMaxLength(14);

                personalDocumentBuilder
                    .HasIndex(x => x.DocumentId)
                    .IsUnique();

                personalDocumentBuilder
                    .Property(y => y.DocumentType);
            });

            builder
                .Property(x => x.IsAnonymous);

            builder
                .Property(x => x.CreatedAtUtc)
                .IsRequired()
                .HasPrecision(7);

            builder
                .Property(x => x.UpdatedAtUtc)
                .IsRequired()
                .HasPrecision(7);
        }
    }
}
