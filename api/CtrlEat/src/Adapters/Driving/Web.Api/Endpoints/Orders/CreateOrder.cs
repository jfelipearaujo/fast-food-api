using Domain.UseCases.Orders.CreateOrder;

using Web.Api.Endpoints.Orders.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class CreateOrder
{
    public static async Task<IResult> HandleAsync(
        CreateOrderEndpointRequest endpointRequest,
        ICreateOrderUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasNotFound())
            {
                return Results.NotFound(result.ToApiError());
            }

            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToCreatedResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.Orders.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }
}