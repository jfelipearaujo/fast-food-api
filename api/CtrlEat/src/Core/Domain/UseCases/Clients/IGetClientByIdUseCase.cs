using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;

using FluentResults;

namespace Domain.UseCases.Clients;

public interface IGetClientByIdUseCase
{
    Task<Result<ClientResponse>> ExecuteAsync(
        GetClientByIdRequest request,
        CancellationToken cancellationToken);
}
