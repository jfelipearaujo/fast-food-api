using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.CreateClient.Requests;
using FluentResults;

namespace Domain.UseCases.Clients.CreateClient;

public interface ICreateClientUseCase
{
    Task<Result<ClientResponse>> ExecuteAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken);
}
