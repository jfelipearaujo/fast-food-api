using Domain.UseCases.Clients.GetAllClients;

using Web.Api.Endpoints.Clients.Mapping;

namespace Web.Api.Endpoints.Clients;

public static class GetAllClients
{
    public static async Task<IResult> HandleAsync(
        IGetAllClientsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}