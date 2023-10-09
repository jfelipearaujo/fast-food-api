using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetClientById.Requests;
using FluentResults;

namespace Domain.UseCases.Clients.GetClientById;

public interface IGetClientByIdUseCase
{
    Task<Result<ClientResponse>> ExecuteAsync(
        GetClientByIdRequest request,
        CancellationToken cancellationToken);
}
