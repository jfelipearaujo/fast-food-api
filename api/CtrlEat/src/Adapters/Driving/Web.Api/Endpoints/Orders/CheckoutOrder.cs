using Domain.UseCases.Orders.CheckoutOrder;
using Domain.UseCases.Orders.CheckoutOrder.Requests;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class CheckoutOrder
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        ICheckoutOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new CheckoutOrderRequest
        {
            OrderId = id
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasNotFound())
            {
                return Results.NotFound(result.ToApiError());
            }

            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}