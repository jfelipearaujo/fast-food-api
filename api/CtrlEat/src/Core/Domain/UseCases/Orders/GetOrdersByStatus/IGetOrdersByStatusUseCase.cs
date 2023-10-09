using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.Common.Responses;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.GetOrdersByStatus;

public interface IGetOrdersByStatusUseCase
{
    Task<Result<Dictionary<OrderStatus, List<OrderResponse>>>> GetOrdersByStatus(
        GetOrdersByStatusRequest request,
        CancellationToken cancellationToken);
}
