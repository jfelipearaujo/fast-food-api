using Domain.Adapters.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class PersistenceDependency
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>();

        // ---

        using var scope = services.BuildServiceProvider().CreateScope();

        var connectionStringBuilder = scope.ServiceProvider.GetRequiredService<IConnectionStringBuilder>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionStringBuilder.Build()));

        return services;
    }
}