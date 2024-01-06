using Domain.UseCases.Orders.GetOrderById;
using Domain.UseCases.Orders.GetOrderById.Requests;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class GetOrderById
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IGetOrderByIdUseCase useCase,
        CancellationToken cancellation)
    {
        var request = new GetOrderByIdRequest
        {
            OrderId = id,
        };

        var result = await useCase.ExecuteAsync(request, cancellation);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}