using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.Errors;
using Domain.Entities.OrderAggregate.ValueObjects;

using Utils.Tests.Builders.Domain.Entities;

namespace Domain.Tests.Entities;

public class OrderTests
{
    [Fact]
    public void ShouldCreateValidOrder()
    {
        // Arrange
        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        // Act
        var result = Order.Create(trackId, client);

        // Assert
        result.Should().BeSuccess();
    }

    [Fact]
    public void ShouldAddItemsIntoTheOrder()
    {
        // Arrange
        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        var order = Order.Create(trackId, client).Value;

        var orderItem = new OrderItemBuilder().WithSample().Build();

        // Act
        order.AddItem(orderItem);

        // Assert
        order.Items.Should().HaveCount(1);
    }

    [Theory]
    [InlineData(OrderStatus.None, OrderStatus.Created, OrderStatus.Created)]
    [InlineData(OrderStatus.Created, OrderStatus.Received, OrderStatus.Received)]
    [InlineData(OrderStatus.Received, OrderStatus.OnGoing, OrderStatus.OnGoing)]
    [InlineData(OrderStatus.OnGoing, OrderStatus.Done, OrderStatus.Done)]
    [InlineData(OrderStatus.Done, OrderStatus.Completed, OrderStatus.Completed)]
    public void ShouldUpdateOrderStatusCorrectly(
        OrderStatus currentStatus,
        OrderStatus toStatus,
        OrderStatus expected)
    {
        // Arrange
        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        var order = Order.Create(trackId, client, currentStatus).Value;

        // Act
        var result = order.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeSuccess();
        order.Status.Should().Be(expected);
        order.StatusUpdatedAt.Should().NotBe(default);
    }

    [Fact]
    public void ShouldNotUpdateStatusToTheSameStatus()
    {
        // Arrange
        var currentStatus = OrderStatus.Created;
        var toStatus = OrderStatus.Created;

        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        var order = Order.Create(trackId, client, currentStatus).Value;

        // Act
        var result = order.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new OrderAlreadyWithStatusError(toStatus));
    }

    [Fact]
    public void ShouldNotUpdateStatusToAnInvalid()
    {
        // Arrange
        var currentStatus = OrderStatus.Created;
        var toStatus = OrderStatus.Done;

        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        var order = Order.Create(trackId, client, currentStatus).Value;

        // Act
        var result = order.UpdateToStatus(toStatus);

        // Assert
        result.Should().BeFailure();
        result.Should().HaveReason(new OrderInvalidStatusTransitionError(currentStatus, toStatus));
    }

    [Fact]
    public void ShouldCalculateCorrectTotalAmount()
    {
        // Arrange
        var trackId = TrackId.CreateUnique();
        var client = new ClientBuilder().WithSample().Build();

        var order = Order.Create(trackId, client).Value;

        var orderItem = new OrderItemBuilder()
            .WithSample()
            .WithQuantity(4)
            .WithProduct(new ProductBuilder()
                .WithSample()
                .WithPrice("BRL", 55)
                .WithProductCategory(new ProductCategoryBuilder()
                    .WithSample()
                    .Build())
                .Build())
            .Build();

        // Act
        order.AddItem(orderItem);

        // Assert
        order.GetTotalPrice().Should().Be("R$ 220,00");
    }
}