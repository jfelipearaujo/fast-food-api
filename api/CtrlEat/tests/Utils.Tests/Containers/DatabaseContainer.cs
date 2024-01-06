using Testcontainers.PostgreSql;

namespace Utils.Tests.Containers;

public static class DatabaseContainer
{
    public static PostgreSqlBuilder DefaultContainer => new PostgreSqlBuilder()
            .WithImage("postgres:16.0")
            .WithDatabase("AppDbCtrlEat")
            .WithUsername("postgres")
            .WithPassword("StrongPassword123")
            .WithCleanUp(true);
}
