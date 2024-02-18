using Domain.UseCases.Orders.UpdateOrderStatus;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class UpdateOrderStatus
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateOrderStatusEndpointRequest endpointRequest,
        IUpdateOrderStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.Ok();
    }
}