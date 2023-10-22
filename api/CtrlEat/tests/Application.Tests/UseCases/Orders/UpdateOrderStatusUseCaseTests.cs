using Application.UseCases.Orders.Common.Errors;
using Application.UseCases.Orders.UpdateOrderStatus;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class UpdateOrderStatusUseCaseTests
{
    private readonly IOrderRepository repository;

    private readonly UpdateOrderStatusUseCase sut;

    public UpdateOrderStatusUseCaseTests()
    {
        repository = Substitute.For<IOrderRepository>();

        sut = new UpdateOrderStatusUseCase(repository);
    }

    [Fact]
    public async Task ShouldUpdateOrderStatusSuccessfully()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new UpdateOrderStatusRequestBuilder()
            .WithOrderId(order.Id)
            .WithStatus(OrderStatus.Received)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await repository
            .Received(1)
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderIsNotFound()
    {
        // Arrange
        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(default(Order));

        var request = new UpdateOrderStatusRequestBuilder()
            .WithSample()
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderNotFoundError(request.OrderId));

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenReceiveAnInvalidStatus()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new UpdateOrderStatusRequestBuilder()
            .WithOrderId(order.Id)
            .WithStatus("InvalidStatus")
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new InvalidOrderStatusError(request.Status));

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenTryToUpdateToAnInvalidPossibleStatus()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new UpdateOrderStatusRequestBuilder()
            .WithOrderId(order.Id)
            .WithStatus(OrderStatus.Completed)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await repository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }
}
