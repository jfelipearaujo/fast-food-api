using Domain.UseCases.Orders.UpdateOrderStatus.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.UpdateOrderStatus;

public interface IUpdateOrderStatusUseCase
{
    Task<Result> ExecuteAsync(
        UpdateOrderStatusRequest request,
        CancellationToken cancellationToken);
}
