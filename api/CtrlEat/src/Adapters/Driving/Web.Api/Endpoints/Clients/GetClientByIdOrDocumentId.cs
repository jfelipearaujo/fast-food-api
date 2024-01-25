using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetClientByDocumentId;
using Domain.UseCases.Clients.GetClientByDocumentId.Requests;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Clients.GetClientById.Requests;

using FluentResults;

using Microsoft.AspNetCore.Mvc;

using Web.Api.Endpoints.Clients.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Clients;

public static class GetClientByIdOrDocumentId
{
    public static async Task<IResult> HandleAsync(
        [FromRoute(Name = "id")] string idOrDocumentId,
        IGetClientByIdUseCase getClientByIdUseCase,
        IGetClientByDocumentIdUseCase getClientByDocumentIdUseCase,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(idOrDocumentId))
        {
            return Results.BadRequest("Id or DocumentId is required");
        }

        Result<ClientResponse>? result;

        if (Guid.TryParse(idOrDocumentId, out Guid id))
        {
            var request = new GetClientByIdRequest
            {
                Id = id,
            };

            result = await getClientByIdUseCase.ExecuteAsync(request, cancellationToken);
        }
        else
        {
            var request = new GetClientByDocumentIdRequest
            {
                DocumentId = idOrDocumentId,
            };

            result = await getClientByDocumentIdUseCase.ExecuteAsync(request, cancellationToken);
        }

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}