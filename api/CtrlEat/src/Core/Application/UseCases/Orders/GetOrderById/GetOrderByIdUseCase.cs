using Application.UseCases.Orders.GetOrderById.Errors;

using Domain.Adapters;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders;
using Domain.UseCases.Orders.Requests;
using Domain.UseCases.Orders.Responses;

using FluentResults;

namespace Application.UseCases.Orders.GetOrderById;

public class GetOrderByIdUseCase : IGetOrderByIdUseCase
{
    private readonly IOrderRepository repository;

    public GetOrderByIdUseCase(IOrderRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<OrderResponse>> ExecuteAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(
            OrderId.Create(request.Id),
            cancellationToken);

        if (order is null)
        {
            return Result.Fail(new OrderNotFoundError(request.Id));
        }

        return OrderResponse.MapFromDomain(order);
    }
}
