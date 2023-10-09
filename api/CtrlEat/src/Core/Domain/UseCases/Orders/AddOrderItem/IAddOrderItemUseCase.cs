using Domain.UseCases.Orders.AddOrderItem.Requests;
using Domain.UseCases.Orders.Common.Responses;

using FluentResults;

namespace Domain.UseCases.Orders.AddOrderItem;

public interface IAddOrderItemUseCase
{
    Task<Result<OrderItemResponse>> ExecuteAsync(
        AddOrderItemRequest request,
        CancellationToken cancellationToken);
}