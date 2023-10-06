using Domain.UseCases.Orders.Requests;
using Domain.UseCases.Orders.Responses;

using FluentResults;

namespace Domain.UseCases.Orders;

public interface IGetOrderByIdUseCase
{
    Task<Result<OrderResponse>> ExecuteAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken);
}