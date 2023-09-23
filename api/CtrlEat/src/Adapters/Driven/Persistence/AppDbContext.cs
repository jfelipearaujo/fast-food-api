using Domain.Adapters.Models;

using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategoryModel> ProductCategory { get; set; }

        public DbSet<ProductModel> Product { get; set; }

        public DbSet<ClientModel> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<IBaseModel>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAtUtc = now;
                }

                entity.Entity.UpdatedAtUtc = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}