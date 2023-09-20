using Domain.Entities;

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

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(250);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(250);

            builder
                .Property(x => x.Email)
                .HasMaxLength(250);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.DocumentId)
                .HasMaxLength(14);

            builder
                .HasIndex(x => x.DocumentId)
                .IsUnique();

            builder
                .Property(x => x.DocumentType);

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
