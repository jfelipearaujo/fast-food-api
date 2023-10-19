using Domain.UseCases.Orders.CheckoutOrder.Requests;
using Domain.UseCases.Orders.CheckoutOrder.Responses;

using FluentResults;

namespace Domain.UseCases.Orders.CheckoutOrder;

public interface ICheckoutOrderUseCase
{
    Task<Result<CheckoutOrderResponse>> ExecuteAsync(
        CheckoutOrderRequest request,
        CancellationToken cancellationToken);
}
