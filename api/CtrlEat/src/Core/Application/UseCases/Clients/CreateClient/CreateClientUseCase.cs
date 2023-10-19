using Application.UseCases.Clients.CreateClient.Errors;
using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.CreateClient;
using Domain.UseCases.Clients.CreateClient.Requests;
using FluentResults;

namespace Application.UseCases.Clients.CreateClient;

public class CreateClientUseCase : ICreateClientUseCase
{
    private readonly IClientRepository repository;

    public CreateClientUseCase(IClientRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ClientResponse>> ExecuteAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        var clientValidation = Client.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DocumentId);

        if (clientValidation.IsFailed)
        {
            return Result.Fail(clientValidation.Errors);
        }

        Client client = clientValidation.Value;

        if (client.FullName.HasData() && !client.Email.HasData())
        {
            return Result.Fail(new ClientRegistrationWithoutEmailError());
        }

        if (client.DocumentId.HasData())
        {
            var documentIdExists = await repository.GetByDocumentIdAsync(
                client.DocumentId,
                cancellationToken);

            if (documentIdExists is not null)
            {
                return Result.Fail(new ClientRegistrationDocumentIdAlreadyExistsError());
            }
        }

        if (client.Email.HasData())
        {
            var emailExists = await repository.GetByEmailAsync(
                client.Email,
                cancellationToken);

            if (emailExists is not null)
            {
                return Result.Fail(new ClientRegistrationEmailAlreadyExistsError());
            }
        }

        await repository.CreateAsync(client, cancellationToken);

        return ClientResponse.MapFromDomain(client);
    }
}
