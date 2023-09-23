using Domain.Adapters;
using Domain.Entities;
using Domain.Errors.Clients;
using Domain.UseCases.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;
using Domain.ValueObjects.Extensions;

using FluentResults;

namespace Application.UseCases.Clients
{
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
            var clientValidation = Client.ValidateAndCreate(
                request.FirstName,
                request.LastName,
                request.Email,
                request.DocumentId);

            if (clientValidation.IsFailed)
            {
                return Result.Fail(clientValidation.Errors);
            }

            Client client = clientValidation.Value;

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
}
