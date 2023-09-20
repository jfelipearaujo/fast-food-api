using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;

using FluentResults;

namespace Domain.UseCases.Clients
{
    public interface ICreateClientUseCase
    {
        Task<Result<ClientResponse>> ExecuteAsync(
            CreateClientRequest request,
            CancellationToken cancellationToken);
    }
}
