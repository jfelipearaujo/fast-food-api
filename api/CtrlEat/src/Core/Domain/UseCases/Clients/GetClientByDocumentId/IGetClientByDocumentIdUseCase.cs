using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetClientByDocumentId.Requests;

using FluentResults;

namespace Domain.UseCases.Clients.GetClientByDocumentId;

public interface IGetClientByDocumentIdUseCase
{
    Task<Result<ClientResponse>> ExecuteAsync(
        GetClientByDocumentIdRequest request,
        CancellationToken cancellationToken);
}
