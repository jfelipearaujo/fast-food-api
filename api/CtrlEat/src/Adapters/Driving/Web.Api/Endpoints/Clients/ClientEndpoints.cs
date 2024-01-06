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

        group.MapGet("{id}", GetClientById.HandleAsync)
            .WithName(ApiEndpoints.Clients.V1.GetById)
            .WithDescription("Searches and returns a client's data based on their identifier")
            .Produces<ClientEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/", GetAllClients.HandleAsync)
            .WithName(ApiEndpoints.Clients.V1.GetAll)
            .WithDescription("Searches and returns all registered client data")
            .Produces<List<ClientEndpointResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", CreateClient.HandleAsync)
            .WithName(ApiEndpoints.Clients.V1.Create)
            .WithDescription("Register a client")
            .Produces<ClientEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);
    }
}
