using Testcontainers.PostgreSql;

using Utils.Tests.Containers;

namespace Infrastructure.Tests;

public class CustomDbFactory : IAsyncLifetime
{
    public PostgreSqlContainer DbContainer { get; private set; }

    public CustomDbFactory()
    {
        DbContainer = DatabaseContainer.DefaultContainer.Build();
    }

    public async Task InitializeAsync()
    {
        await DbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContainer.StopAsync();
    }
}
