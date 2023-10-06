using Domain.UseCases.Orders.Requests;
using Domain.UseCases.Orders.Responses;

using FluentResults;

namespace Domain.UseCases.Orders;

public interface IAddOrderItemUseCase
{
    Task<Result<OrderItemResponse>> ExecuteAsync(
        AddOrderItemRequest request,
        CancellationToken cancellationToken);
}