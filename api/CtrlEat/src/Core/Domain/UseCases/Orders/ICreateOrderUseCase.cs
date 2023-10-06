using Domain.UseCases.Orders.Requests;
using Domain.UseCases.Orders.Responses;

using FluentResults;

namespace Domain.UseCases.Orders;

public interface ICreateOrderUseCase
{
    Task<Result<OrderResponse>> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken);
}
