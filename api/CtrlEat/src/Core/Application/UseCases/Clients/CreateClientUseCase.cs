using Domain.Adapters;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors.Clients;
using Domain.UseCases.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;
using Domain.Validators;

using FluentResults;

using Mapster;

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
            var hasFirstName = !string.IsNullOrEmpty(request.FirstName?.Trim());
            var hasLastName = !string.IsNullOrEmpty(request.LastName?.Trim());
            var hasEmail = !string.IsNullOrEmpty(request.Email?.Trim());
            var hasDocumentId = !string.IsNullOrEmpty(request.DocumentId?.Trim());

            var isAnonymous = !(hasFirstName || hasLastName || hasEmail || hasDocumentId);

            if (!hasFirstName && hasLastName)
            {
                return Result.Fail(new ClientRegistrationWithoutFirstNameError());
            }

            if (hasFirstName && hasLastName && !hasEmail)
            {
                return Result.Fail(new ClientRegistrationWithoutEmailError());
            }

            if (hasDocumentId && !Cpf.Check(request.DocumentId))
            {
                return Result.Fail(new ClientRegistrationInvalidDocumentIdError());
            }

            if (hasEmail)
            {
                var emailExists = await repository.GetByEmailAsync(request.Email, cancellationToken);

                if (emailExists is not null)
                {
                    return Result.Fail(new ClientRegistrationEmailAlreadyExistsError());
                }
            }

            if (hasDocumentId)
            {
                var documentIdExists = await repository.GetByDocumentIdAsync(request.DocumentId, cancellationToken);

                if (documentIdExists is not null)
                {
                    return Result.Fail(new ClientRegistrationDocumentIdAlreadyExistsError());
                }
            }

            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DocumentType = isAnonymous ? DocumentType.None : DocumentType.CPF,
                DocumentId = request.DocumentId,
                IsAnonymous = isAnonymous,
            };

            await repository.CreateAsync(client, cancellationToken);

            var response = client.Adapt<ClientResponse>();

            return Result.Ok(response);
        }
    }
}
