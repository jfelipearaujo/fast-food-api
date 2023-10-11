using Domain.UseCases.Orders.Common.Responses;
using Domain.UseCases.Orders.GetOrderById.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<Result<OrderResponse>> ExecuteAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken);
}