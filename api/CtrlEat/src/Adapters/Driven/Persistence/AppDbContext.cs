using Domain.Common.Models;
using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.ProductAggregate;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

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

    public DbSet<ProductCategory> ProductCategory { get; set; }

    public DbSet<Product> Product { get; set; }

    public DbSet<Stock> Stock { get; set; }

    public DbSet<Client> Client { get; set; }

    public DbSet<Order> Order { get; set; }

    public DbSet<OrderItem> OrderItem { get; set; }

    public DbSet<Payment> Payment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker.Entries<IEntity>()
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