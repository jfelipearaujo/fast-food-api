using Domain.UseCases.Orders.CheckoutHookOrder;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class CheckoutHookOrder
{
    public static async Task<IResult> HandleAsync(
        CheckoutHookOrderEndpointRequest endpointRequest,
        ICheckoutHookOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.Ok();
    }
}