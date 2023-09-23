using Domain.Adapters.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder.ToTable("client");

            builder.HasKey(x => x.Id);

            builder
                .Property(y => y.FirstName)
                .HasMaxLength(250);

            builder
                .Property(y => y.LastName)
                .HasMaxLength(250);

            builder
                .Property(y => y.Email)
                .HasMaxLength(250);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(y => y.DocumentId)
                .HasMaxLength(14);

            builder
                .HasIndex(x => x.DocumentId)
                .IsUnique();

            builder
                .Property(y => y.DocumentType);

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
