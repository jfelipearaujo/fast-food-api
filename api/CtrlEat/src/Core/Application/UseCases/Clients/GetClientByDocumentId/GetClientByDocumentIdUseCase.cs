using Application.UseCases.Common.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.GetClientByDocumentId;
using Domain.UseCases.Clients.GetClientByDocumentId.Requests;

using FluentResults;

namespace Application.UseCases.Clients.GetClientByDocumentId;

public class GetClientByDocumentIdUseCase : IGetClientByDocumentIdUseCase
{
    private readonly IClientRepository clientRepository;

    public GetClientByDocumentIdUseCase(IClientRepository clientRepository)
    {
        this.clientRepository = clientRepository;
    }

    public async Task<Result<ClientResponse>> ExecuteAsync(
        GetClientByDocumentIdRequest request,
        CancellationToken cancellationToken)
    {
        var documentId = DocumentId.Create(request.DocumentId);

        if (documentId.IsFailed)
        {
            return Result.Fail(documentId.Errors);
        }

        var client = await clientRepository.GetByDocumentIdAsync(documentId.Value, cancellationToken);

        if (client is null)
        {
            return Result.Fail(new ClientNotFoundByDocumentIdError(request.DocumentId));
        }

        return ClientResponse.MapFromDomain(client);
    }
}
