using Testcontainers.PostgreSql;

namespace Infrastructure.Tests;

public class CustomDbFactory : IAsyncLifetime
{
    public PostgreSqlContainer DbContainer { get; private set; }

    public CustomDbFactory()
    {
        DbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16.0")
            .WithDatabase("AppDbCtrlEat")
            .WithUsername("postgres")
            .WithPassword("StrongPassword123")
            .Build();
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
