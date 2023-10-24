using Domain.UseCases.Clients.CreateClient;
using Domain.UseCases.Clients.GetAllClients;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Clients.GetClientById.Requests;

using Web.Api.Endpoints.Clients.Requests;
using Web.Api.Endpoints.Clients.Requests.Mapping;
using Web.Api.Endpoints.Clients.Responses;
using Web.Api.Endpoints.Clients.Responses.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Clients;

public static class ClientEndpoints
{
    private const string EndpointTag = "Client";

    public static void MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var clients = app.NewVersionedApi(EndpointTag);

        var group = clients.MapGroup(ApiEndpoints.Clients.BaseRoute)
            .HasApiVersion(ApiEndpoints.Clients.V1.Version)
            .WithOpenApi();

        group.MapGet("{id}", GetClientById)
            .WithName(ApiEndpoints.Clients.V1.GetById)
            .WithDescription("Searches and returns a client's data based on their identifier")
            .Produces<ClientEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllClients)
            .WithName(ApiEndpoints.Clients.V1.GetAll)
            .WithDescription("Searches and returns all registered client data")
            .Produces<List<ClientEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateClient)
            .WithName(ApiEndpoints.Clients.V1.Create)
            .WithDescription("Register a client")
            .Produces<ClientEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);
    }

    public static async Task<IResult> GetClientById(
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
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> GetAllClients(
        IGetAllClientsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateClient(
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
