using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

using Infrastructure.Repositories;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data.Common;

using Utils.Tests.Builders.Domain.Entities;

namespace Infrastructure.Tests.Repositories;

public class OrderRepositoryTests : IDisposable
{
    private readonly OrderRepository sut;

    private readonly AppDbContext dbContext;

    private readonly DbConnection dbConnection;

    private readonly DbContextOptions<AppDbContext> dbContextOptions;

    private Client client;

    public OrderRepositoryTests()
    {
        dbConnection = new SqliteConnection("Filename=:memory:");
        dbConnection.Open();

        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(dbConnection)
            .Options;

        dbContext = new AppDbContext(dbContextOptions);

        dbContext.Database.Migrate();

        client = new ClientBuilder().WithSample().Build();

        dbContext.Client.Add(client);
        dbContext.SaveChanges();

        sut = new OrderRepository(dbContext);
    }

    [Fact]
    public async Task GetAllByStatusSuccessfullyWhenSearchForSpecificStatus()
    {
        // Arrange
        var ordersCount = 10;
        var orders = new List<Order>();

        for (int i = 0; i < ordersCount; i++)
        {
            orders.Add(Order.Create(client, OrderStatus.Created).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllByStatusAsync(OrderStatus.Created, default);

        // Assert
        var expectedResult = new Dictionary<OrderStatus, List<Order>>
        {
            { OrderStatus.Created, orders }
        };

        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetAllByStatusSuccessfullyWhenSearchAnyStatus()
    {
        // Arrange
        var ordersCreatedCount = 5;
        var ordersReceivedCount = 5;
        var orders = new List<Order>();

        for (int i = 0; i < ordersCreatedCount; i++)
        {
            orders.Add(Order.Create(client, OrderStatus.Created).Value);
        }

        for (int i = 0; i < ordersReceivedCount; i++)
        {
            orders.Add(Order.Create(client, OrderStatus.Received).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllByStatusAsync(OrderStatus.None, default);

        // Assert
        var expectedResult = new Dictionary<OrderStatus, List<Order>>
        {
            { OrderStatus.Created, orders.Where(x => x.Status == OrderStatus.Created).ToList() },
            { OrderStatus.Received, orders.Where(x => x.Status == OrderStatus.Received).ToList() },
        };

        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedResult);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            dbConnection.Dispose();
            dbContext.Dispose();
        }
    }
}
