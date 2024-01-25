using Domain.UseCases.Orders.AddOrderItem;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class AddOrderItem
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        AddOrderItemEndpointRequest endpointRequest,
        IAddOrderItemUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}