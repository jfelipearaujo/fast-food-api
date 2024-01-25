using Domain.UseCases.Orders.CheckoutHookOrder.Requests;

using FluentResults;

namespace Domain.UseCases.Orders.CheckoutHookOrder;

public interface ICheckoutHookOrderUseCase
{
    Task<Result> ExecuteAsync(
        CheckoutHookOrderRequest request,
        CancellationToken cancellationToken);
}
