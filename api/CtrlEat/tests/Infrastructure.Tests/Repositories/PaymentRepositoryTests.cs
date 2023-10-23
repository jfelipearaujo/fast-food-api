using Domain.Entities.ClientAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.ProductAggregate.ValueObjects;

using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Utils.Tests.Builders.Domain.Entities;

namespace Infrastructure.Tests.Repositories;

public class PaymentRepositoryTests : IClassFixture<CustomDbFactory>, IDisposable
{
    private readonly PaymentRepository sut;

    private readonly AppDbContext dbContext;

    private readonly DbContextOptions<AppDbContext> dbContextOptions;

    private readonly Client client;
    private readonly Order order;

    public PaymentRepositoryTests(CustomDbFactory customDbFactory)
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(customDbFactory.DbContainer.GetConnectionString())
            .Options;

        dbContext = new AppDbContext(dbContextOptions);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        client = new ClientBuilder()
            .WithSample()
            .Build();

        dbContext.Client.Add(client);

        order = new OrderBuilder()
            .WithSample()
            .WithClient(client)
            .Build();

        dbContext.Order.Add(order);

        dbContext.SaveChanges();

        sut = new PaymentRepository(dbContext);
    }

    [Fact]
    public async Task ShouldCreateSuccessfully()
    {
        // Arrange
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(order.Id, price).Value;

        // Act
        var result = await sut.CreateAsync(payment, default);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task ShouldGetByIdSuccessfully()
    {
        // Arrange
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(order.Id, price).Value;

        await sut.CreateAsync(payment, default);

        // Act
        var result = await sut.GetByIdAsync(payment.Id, default);

        // Assert
        result.Should().BeEquivalentTo(payment);
    }

    [Fact]
    public async Task ShouldGetAllSuccessfully()
    {
        // Arrange
        var paymentsCount = 10;
        var payments = new List<Payment>();

        for (int i = 0; i < paymentsCount; i++)
        {
            var price = Money.Create(100, Money.BRL).Value;

            payments.Add(Payment.Create(order.Id, price).Value);
        }

        dbContext.Payment.AddRange(payments);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAllAsync(default);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(payments);
    }

    [Fact]
    public async Task ShouldDeleteSuccessfully()
    {
        // Arrange
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(order.Id, price).Value;

        await sut.CreateAsync(payment, default);

        // Act
        var result = await sut.DeleteAsync(payment, default);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task ShouldUpdateSuccessfully()
    {
        // Arrange
        var price = Money.Create(100, Money.BRL).Value;

        var payment = Payment.Create(order.Id, price).Value;

        await sut.CreateAsync(payment, default);

        payment.UpdateToStatus(PaymentStatus.Approved);

        // Act
        var result = await sut.UpdateAsync(payment, default);

        // Assert
        result.Should().Be(1);
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
