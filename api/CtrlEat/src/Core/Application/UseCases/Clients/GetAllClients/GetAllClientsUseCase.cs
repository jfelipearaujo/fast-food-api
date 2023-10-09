using Domain.Adapters;
using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetAllClients;
using FluentResults;

namespace Application.UseCases.Clients.GetAllClients;

public class GetAllClientsUseCase : IGetAllClientsUseCase
{
    private readonly IClientRepository repository;

    public GetAllClientsUseCase(IClientRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<ClientResponse>>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var clients = await repository.GetAllAsync(cancellationToken);

        var response = new List<ClientResponse>();

        foreach (var client in clients)
        {
            response.Add(ClientResponse.MapFromDomain(client));
        }

        return response;
    }
}
