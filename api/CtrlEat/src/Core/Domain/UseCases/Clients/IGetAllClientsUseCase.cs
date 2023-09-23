using Domain.UseCases.Clients.Responses;

using FluentResults;

namespace Domain.UseCases.Clients
{
    public interface IGetAllClientsUseCase
    {
        Task<Result<List<ClientResponse>>> ExecuteAsync(
            CancellationToken cancellationToken);
    }
}
