using Domain.UseCases.Clients.CreateClient;

using Web.Api.Endpoints.Clients.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Clients;

public static class CreateClient
{
    public static async Task<IResult> HandleAsync(
        CreateClientEndpointRequest endpointRequest,
        ICreateClientUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.Clients.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }
}