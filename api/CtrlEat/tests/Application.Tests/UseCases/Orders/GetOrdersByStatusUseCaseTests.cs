using Application.UseCases.Orders.Common.Errors;
using Application.UseCases.Orders.GetOrdersByStatus;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.Common.Responses;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Application.Orders.Responses;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class GetOrdersByStatusUseCaseTests
{
    private readonly IOrderRepository repository;

    private readonly GetOrdersByStatusUseCase sut;

    public GetOrdersByStatusUseCaseTests()
    {
        repository = Substitute.For<IOrderRepository>();

        sut = new GetOrdersByStatusUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetOrdersBySpecificStatusSuccessfully()
    {
        // Arrange
        var statusToSearch = OrderStatus.Created;

        var request = new GetOrdersByStatusRequestBuilder()
           .WithStatus(statusToSearch)
           .Build();

        var orders = new List<Order>
        {
            new OrderBuilder().WithSample().WithStatus(statusToSearch).Build(),
            new OrderBuilder().WithSample().WithStatus(statusToSearch).Build(),
            new OrderBuilder().WithSample().WithStatus(statusToSearch).Build(),
        };

        repository
            .GetAllByStatusAsync(statusToSearch, default)
            .Returns(orders);

        var expected = new List<OrderTrackingResponse>
        {
            new OrderTrackingResponseBuilder()
               .WithStatus(statusToSearch)
               .WithOrders(orders)
               .Build()
        };

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();
        response.Should().Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expected,
                opt => opt.For(x => x.Orders).Exclude(y => y.StatusUpdatedAtFormatted));
        });

        await repository
            .Received(1)
            .GetAllByStatusAsync(statusToSearch, default);
    }

    [Fact]
    public async Task ShouldGetOrdersByWithoutSpecifingStatusSuccessfully()
    {
        // Arrange
        var request = new GetOrdersByStatusRequestBuilder()
           .WithStatus(null)
           .Build();

        var orders = new List<Order>
        {
            new OrderBuilder().WithSample().WithStatus(OrderStatus.Created).Build(),
            new OrderBuilder().WithSample().WithStatus(OrderStatus.OnGoing).Build(),
            new OrderBuilder().WithSample().WithStatus(OrderStatus.Completed).Build(),
        };

        repository
            .GetAllByStatusAsync(Arg.Any<OrderStatus>(), default)
            .Returns(orders);

        var expected = new List<OrderTrackingResponse>
        {
            new OrderTrackingResponseBuilder()
               .WithStatus(OrderStatus.Created)
               .WithOrders(orders.Where(x => x.Status == OrderStatus.Created).ToList())
               .Build(),
            new OrderTrackingResponseBuilder()
               .WithStatus(OrderStatus.OnGoing)
               .WithOrders(orders.Where(x => x.Status == OrderStatus.OnGoing).ToList())
               .Build(),
            new OrderTrackingResponseBuilder()
               .WithStatus(OrderStatus.Completed)
               .WithOrders(orders.Where(x => x.Status == OrderStatus.Completed).ToList())
               .Build(),
        };

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();
        response.Should().Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expected,
                opt => opt.For(x => x.Orders).Exclude(y => y.StatusUpdatedAtFormatted));
        });

        await repository
            .Received(1)
            .GetAllByStatusAsync(Arg.Any<OrderStatus>(), default);
    }

    [Fact]
    public async Task ShouldReturnErrorWhenAnInvalidStatusIsProvided()
    {
        // Arrange
        var request = new GetOrdersByStatusRequestBuilder()
           .WithStatus("InvalidStatus")
           .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new InvalidOrderStatusError(request.Status));

        await repository
            .DidNotReceive()
            .GetAllByStatusAsync(Arg.Any<OrderStatus>(), default);
    }

}
