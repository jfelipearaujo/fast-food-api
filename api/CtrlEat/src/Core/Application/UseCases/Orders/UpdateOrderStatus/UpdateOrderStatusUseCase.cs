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

        var newStatus = OrderStatus.None;

        if (!string.IsNullOrEmpty(request.Status) && !Enum.TryParse(request.Status, true, out newStatus))
        {
            return Result.Fail(new InvalidOrderStatusError(request.Status));
        }

        var updateStatusResult = order.UpdateToStatus(newStatus);

        if (updateStatusResult.IsFailed)
        {
            return Result.Fail(updateStatusResult.Errors);
        }

        await repository.UpdateAsync(order, cancellationToken);

        return Result.Ok();
    }
}
