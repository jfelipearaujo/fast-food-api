using Application.UseCases.Orders.Common.Errors;

using Domain.Adapters;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.UpdateOrderStatus;
using Domain.UseCases.Orders.UpdateOrderStatus.Requests;

using FluentResults;

namespace Application.UseCases.Orders.UpdateOrderStatus;


public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
{
    private readonly IOrderRepository repository;

    public UpdateOrderStatusUseCase(IOrderRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> ExecuteAsync(
        UpdateOrderStatusRequest request,
        CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(OrderId.Create(request.OrderId), cancellationToken);

        if (order is null)
        {
            return Result.Fail(new OrderNotFoundError(request.OrderId));
        }

        var statusToUpdate = OrderStatus.None;

        if (!string.IsNullOrEmpty(request.Status) && !Enum.TryParse(request.Status, true, out statusToUpdate))
        {
            return Result.Fail(new InvalidOrderStatusError(request.Status));
        }

        if (statusToUpdate == OrderStatus.None)
        {
            return Result.Fail(new InvalidOrderStatusForUpdateError(request.Status));
        }

        var newStatus = OrderStatusStateMachine.MoveTo(order.Status, statusToUpdate);

        if (newStatus.IsFailed)
        {
            return Result.Fail(newStatus.Errors);
        }

        order.UpdateStatus(newStatus.Value);
        await repository.UpdateAsync(order, cancellationToken);

        return Result.Ok();
    }
}
