using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate;
using Domain.UseCases.Orders;
using Domain.UseCases.Orders.Requests;
using Domain.UseCases.Orders.Responses;

using FluentResults;

namespace Application.UseCases.Orders.CreateOrder;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository orderRepository;
    private readonly IClientRepository clientRepository;

    public CreateOrderUseCase(
        IOrderRepository orderRepository,
        IClientRepository clientRepository)
    {
        this.orderRepository = orderRepository;
        this.clientRepository = clientRepository;
    }

    public async Task<Result<OrderResponse>> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(
            ClientId.Create(request.ClientId),
            cancellationToken);

        if (client is null)
        {
            return Result.Fail(new ClientNotFoundError(request.ClientId));
        }

        var order = Order.Create(client);

        await orderRepository.CreateAsync(order.Value, cancellationToken);

        return OrderResponse.MapFromDomain(order.Value);
    }
}
