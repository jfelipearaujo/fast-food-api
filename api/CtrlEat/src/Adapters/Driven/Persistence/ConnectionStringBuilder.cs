using Domain.Adapters.Database;

using Npgsql;

namespace Persistence;

public class ConnectionStringBuilder : IConnectionStringBuilder
{
    private readonly string? connectionString;

    public ConnectionStringBuilder()
    {
    }

    public ConnectionStringBuilder(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public string Build()
    {
        if (connectionString is not null)
        {
            return connectionString;
        }

#if DEBUG
        //Environment.SetEnvironmentVariable("DB_NAME", "AppDbCtrlEat");
        //Environment.SetEnvironmentVariable("DB_HOST", "localhost");
        //Environment.SetEnvironmentVariable("DB_PORT", "5432");
        //Environment.SetEnvironmentVariable("DB_USER", "postgres");
        //Environment.SetEnvironmentVariable("DB_PASS", "StrongPassword123");
#endif

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Database = Environment.GetEnvironmentVariable("DB_NAME"),
            Host = Environment.GetEnvironmentVariable("DB_HOST"),
            Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT")),
            Username = Environment.GetEnvironmentVariable("DB_USER"),
            Password = Environment.GetEnvironmentVariable("DB_PASS")
        };

        return connectionStringBuilder.ConnectionString;
    }
}
