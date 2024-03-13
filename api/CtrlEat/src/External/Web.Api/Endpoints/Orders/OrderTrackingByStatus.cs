using Domain.UseCases.Orders.GetOrdersByStatus;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class OrderTrackingByStatus
{
    public static async Task<IResult> HandleAsync(
        string? status,
        IGetOrdersByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetOrdersByStatusRequest
        {
            Status = status
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}