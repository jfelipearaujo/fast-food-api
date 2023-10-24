using DotNet.Testcontainers.Builders;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Persistence;

using Testcontainers.PostgreSql;

using Web.Api.Markers;

namespace Contract.Tests;

public class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer dbContainer;

    public ApiFactory()
    {
        dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16.0")
            .WithEnvironment("POSTGRES_DB", "AppDbCtrlEat")
            .WithEnvironment("POSTGRES_USER", "postgres")
            .WithEnvironment("POSTGRES_PASSWORD", "StrongPassword123")
            .WithPortBinding(5432)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(AppDbContext));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(dbContainer.GetConnectionString());
            });
        });
    }

    public async Task ResetDatabase(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

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
