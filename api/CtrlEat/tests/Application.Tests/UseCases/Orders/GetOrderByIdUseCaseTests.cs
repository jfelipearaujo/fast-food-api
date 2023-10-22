using Application.UseCases.Orders.Common.Errors;
using Application.UseCases.Orders.GetOrderById;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class GetOrderByIdUseCaseTests
{
    private readonly IOrderRepository repository;

    private readonly GetOrderByIdUseCase sut;

    public GetOrderByIdUseCaseTests()
    {
        repository = Substitute.For<IOrderRepository>();

        sut = new GetOrderByIdUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetOrderSuccessfully()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .Build();

        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new GetOrderByIdRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();

        await repository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderIsNotFound()
    {
        // Arrange
        repository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(default(Order));

        var request = new GetOrderByIdRequestBuilder()
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
    }
}
