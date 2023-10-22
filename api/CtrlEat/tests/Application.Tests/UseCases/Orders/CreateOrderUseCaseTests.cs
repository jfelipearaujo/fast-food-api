using Application.UseCases.Common.Errors;
using Application.UseCases.Orders.CreateOrder;

using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class CreateOrderUseCaseTests
{
    private readonly IOrderRepository orderRepository;
    private readonly IClientRepository clientRepository;

    private readonly CreateOrderUseCase sut;

    public CreateOrderUseCaseTests()
    {
        orderRepository = Substitute.For<IOrderRepository>();
        clientRepository = Substitute.For<IClientRepository>();

        sut = new CreateOrderUseCase(orderRepository, clientRepository);
    }

    [Fact]
    public async Task ShouldCreateAnOrderSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder()
            .WithSample()
            .Build();

        var request = new CreateOrderRequestBuilder()
            .WithClientId(client.Id)
            .Build();

        clientRepository
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>())
            .Returns(client);

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();

        await clientRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .Received(1)
            .CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenClientIsNotFound()
    {
        // Arrange
        var request = new CreateOrderRequestBuilder()
            .WithSample()
            .Build();

        clientRepository
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>())
            .Returns(default(Client));

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ClientNotFoundError(request.ClientId));

        await clientRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }
}
