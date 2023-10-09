using Domain.UseCases.Clients.CreateClient;
using Domain.UseCases.Clients.GetAllClients;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Clients.GetClientById.Requests;

using Microsoft.AspNetCore.Http.HttpResults;

using Web.Api.Endpoints.Clients.Mapping;
using Web.Api.Endpoints.Clients.Requests;
using Web.Api.Endpoints.Clients.Responses;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Clients;

public static class ClientEndpoints
{
    private const string EndpointTag = "Client";

    public static void MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clients")
            .WithTags(EndpointTag);

        group.MapGet("{id}", GetClientById)
            .WithName(nameof(GetClientById))
            .WithOpenApi();

        group.MapGet("/", GetAllClients)
            .WithName(nameof(GetAllClients))
            .WithOpenApi();

        group.MapPost("/", CreateClient)
            .WithName(nameof(CreateClient))
            .WithOpenApi();
    }

    public static async Task<Results<Ok<ClientEndpointResponse>, NotFound<ApiError>>> GetClientById(
        Guid id,
        IGetClientByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetClientByIdRequest
        {
            Id = id,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<List<ClientEndpointResponse>>> GetAllClients(
        IGetAllClientsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Results<CreatedAtRoute<ClientEndpointResponse>, BadRequest<ApiError>>> CreateClient(
        CreateClientEndpointRequest endpointRequest,
        ICreateClientUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetClientById),
            new
            {
                id = response.Id,
            });
    }
}
