using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Clients.GetClientById.Requests;

using FluentResults;

namespace Application.UseCases.Clients.GetClientById;

public class GetClientByIdUseCase : IGetClientByIdUseCase
{
    private readonly IClientRepository repository;

    public GetClientByIdUseCase(IClientRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ClientResponse>> ExecuteAsync(
        GetClientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var client = await repository.GetByIdAsync(ClientId.Create(request.Id), cancellationToken);

        if (client is null)
        {
            return Result.Fail(new ClientNotFoundError(request.Id));
        }

        return ClientResponse.MapFromDomain(client);
    }
}
