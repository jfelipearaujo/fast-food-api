using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.StrongIds;
using Domain.Enums;
using Domain.Errors.Clients;
using Domain.UseCases.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;
using Domain.ValueObjects;
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
            var firstName = new Name(request.FirstName);
            var lastName = new Name(request.LastName);
            var documentId = new DocumentId(request.DocumentId);
            var email = new Email(request.Email);

            var firstNameValidation = firstName.Validate();
            var lastNameValidation = lastName.Validate();
            var documentIdValidation = documentId.Validate();
            var emailValidation = email.Validate();

            if (firstNameValidation.IsFailed)
            {
                return Result.Fail(firstNameValidation.Errors);
            }

            if (lastNameValidation.IsFailed)
            {
                return Result.Fail(lastNameValidation.Errors);
            }

            if (documentIdValidation.IsFailed)
            {
                return Result.Fail(documentIdValidation.Errors);
            }

            if (emailValidation.IsFailed)
            {
                return Result.Fail(emailValidation.Errors);
            }

            if (firstName.HasData() && !email.HasData())
            {
                return Result.Fail(new ClientRegistrationWithoutEmailError());
            }

            if (documentId.HasData())
            {
                var documentIdExists = await repository.GetByDocumentIdAsync(
                    documentId,
                    cancellationToken);

                if (documentIdExists is not null)
                {
                    return Result.Fail(new ClientRegistrationDocumentIdAlreadyExistsError());
                }
            }

            if (email.HasData())
            {
                var emailExists = await repository.GetByEmailAsync(
                    email,
                    cancellationToken);

                if (emailExists is not null)
                {
                    return Result.Fail(new ClientRegistrationEmailAlreadyExistsError());
                }
            }

            var isAnonymous = !firstName.HasData() && !email.HasData() && !documentId.HasData();

            var client = new Client
            {
                Id = ClientId.Create(Guid.NewGuid()),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DocumentId = documentId,
                DocumentType = isAnonymous ? DocumentType.None : DocumentType.CPF,
                IsAnonymous = isAnonymous
            };

            await repository.CreateAsync(client, cancellationToken);

            var response = ClientResponse.MapFromDomain(client);

            return Result.Ok(response);
        }
    }
}
