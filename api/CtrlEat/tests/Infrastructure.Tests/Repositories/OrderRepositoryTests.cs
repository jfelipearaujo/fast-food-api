using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Utils.Tests.Builders.Domain.Entities;

namespace Infrastructure.Tests.Repositories;

public class OrderRepositoryTests : IClassFixture<CustomDbFactory>, IDisposable
{
    private readonly OrderRepository sut;

    private readonly AppDbContext dbContext;

    private readonly DbContextOptions<AppDbContext> dbContextOptions;

    private readonly Client client;

    public OrderRepositoryTests(CustomDbFactory customDbFactory)
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(customDbFactory.DbContainer.GetConnectionString())
            .Options;

        dbContext = new AppDbContext(dbContextOptions);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        client = new ClientBuilder().WithSample().Build();

        dbContext.Client.Add(client);
        dbContext.SaveChanges();

        sut = new OrderRepository(dbContext);
    }

    [Fact]
    public async Task CreateSuccessfully()
    {
        // Arrange
        var order = Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value;

        // Act
        var result = await sut.CreateAsync(order, default);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task GetByIdSuccesfully()
    {
        // Arrange
        var order = Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value;

        await sut.CreateAsync(order, default);

        // Act
        var result = await sut.GetByIdAsync(order.Id, default);

        // Assert
        result.Should().BeEquivalentTo(order);
    }

    [Fact]
    public async Task UpdateSuccesfully()
    {
        // Arrange
        var order = Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value;

        await sut.CreateAsync(order, default);

        order.UpdateToStatus(OrderStatus.Received);

        // Act
        var result = await sut.UpdateAsync(order, default);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task DeleteSuccesfully()
    {
        // Arrange
        var order = Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value;

        await sut.CreateAsync(order, default);

        // Act
        var result = await sut.DeleteAsync(order, default);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task GetAllSuccessfully()
    {
        // Arrange
        var ordersCount = 10;
        var orders = new List<Order>();

        for (int i = 0; i < ordersCount; i++)
        {
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllAsync(default);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(orders);
    }

    [Fact]
    public async Task GetAllByStatusSuccessfullyWhenSearchForSpecificStatus()
    {
        // Arrange
        var ordersCount = 10;
        var orders = new List<Order>();

        for (int i = 0; i < ordersCount; i++)
        {
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllByStatusAsync(OrderStatus.Created, default);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(orders);
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
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Created).Value);
        }

        for (int i = 0; i < ordersReceivedCount; i++)
        {
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Received).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllByStatusAsync(OrderStatus.None, default);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(orders);
    }

    [Fact]
    public async Task GetAllByStatusSuccessfullyWhenSearchAnyStatusFilteringTheMostRecent()
    {
        // Arrange
        var ordersCompletedNowCount = 5;
        var ordersCompletedAgoCount = 5;
        var orders = new List<Order>();

        for (int i = 0; i < ordersCompletedNowCount; i++)
        {
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Completed).Value);
        }

        for (int i = 0; i < ordersCompletedAgoCount; i++)
        {
            orders.Add(Order.Create(TrackId.CreateUnique(), client, OrderStatus.Completed, DateTime.UtcNow.AddMinutes(-10)).Value);
        }

        dbContext.Order.AddRange(orders);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllByStatusAsync(OrderStatus.None, default);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(orders.Take(5).ToList());
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
            dbContext.Dispose();
        }
    }
}
