using Contract.Tests.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.PostgreSql;

using Utils.Tests.Containers;

namespace Contract.Tests;

public class ApiFactory<TProgramMarker, TDbContext> : WebApplicationFactory<TProgramMarker>, IAsyncLifetime
    where TProgramMarker : class
    where TDbContext : DbContext
{
    private readonly PostgreSqlContainer dbContainer;

    public ApiFactory()
    {
        dbContainer = DatabaseContainer.DefaultContainer.Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<TDbContext>();

            services.AddDbContext<TDbContext>(options =>
            {
                var cs = dbContainer.GetConnectionString();

                options.UseNpgsql(cs);
            });

            services.EnsureDbCreatedAndMigrated<TDbContext>();
        });
    }

    public async Task ResetDatabase(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
    }

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await dbContainer.StopAsync();
        await dbContainer.DisposeAsync();
    }
}
