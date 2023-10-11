using Domain.UseCases.Clients.Common.Responses;
using FluentResults;

namespace Domain.UseCases.Clients.GetAllClients;

public interface IGetAllClientsUseCase
{
    Task<Result<List<ClientResponse>>> ExecuteAsync(
        CancellationToken cancellationToken);
}
