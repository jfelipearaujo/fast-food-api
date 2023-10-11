using Domain.UseCases.Orders.Common.Responses;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.GetOrdersByStatus;

public interface IGetOrdersByStatusUseCase
{
    Task<Result<List<OrderTrackingResponse>>> ExecuteAsync(
        GetOrdersByStatusRequest request,
        CancellationToken cancellationToken);
}
