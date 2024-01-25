using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Tests.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RemoveService<TService>(this IServiceCollection services) where TService : class
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }
    }

    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }
    }

    public static void EnsureDbCreatedAndMigrated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var context = scopedServices.GetRequiredService<T>();

        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}
