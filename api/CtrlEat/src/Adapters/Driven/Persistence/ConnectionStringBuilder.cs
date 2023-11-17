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
