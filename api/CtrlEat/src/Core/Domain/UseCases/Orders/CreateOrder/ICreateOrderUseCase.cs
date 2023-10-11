using Domain.UseCases.Orders.Common.Responses;
using Domain.UseCases.Orders.CreateOrder.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<Result<OrderResponse>> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken);
}
