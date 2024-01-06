using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Clients.GetClientById.Requests;

using Web.Api.Endpoints.Clients.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Clients;

public static class GetClientById
{
    public static async Task<IResult> HandleAsync(
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
}