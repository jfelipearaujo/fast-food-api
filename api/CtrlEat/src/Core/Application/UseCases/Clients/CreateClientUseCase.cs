using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;
using Domain.Errors.Clients;
using Domain.UseCases.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;
using Domain.ValueObjects;

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
            var fullName = FullName.Create(request.FirstName, request.LastName);
            var personalDocument = Cpf.Create(request.DocumentId);
            var email = Email.Create(request.Email);

            if (fullName.IsFailed)
            {
                return Result.Fail(fullName.Errors);
            }

            if (personalDocument.IsFailed)
            {
                return Result.Fail(personalDocument.Errors);
            }

            if (email.IsFailed)
            {
                return Result.Fail(email.Errors);
            }

            if (fullName.HasData() && !email.HasData())
            {
                return Result.Fail(new ClientRegistrationWithoutEmailError());
            }

            if (personalDocument.HasData())
            {
                var documentIdExists = await repository.GetByDocumentIdAsync(
                    personalDocument.Value.DocumentId,
                    cancellationToken);

                if (documentIdExists is not null)
                {
                    return Result.Fail(new ClientRegistrationDocumentIdAlreadyExistsError());
                }
            }

            if (email.HasData())
            {
                var emailExists = await repository.GetByEmailAsync(
                    email.Value.Address,
                    cancellationToken);

                if (emailExists is not null)
                {
                    return Result.Fail(new ClientRegistrationEmailAlreadyExistsError());
                }
            }

            var client = new Client
            {
                Id = new ClientId(Guid.NewGuid()),
                FullName = fullName.Value,
                PersonalDocument = personalDocument.Value,
                Email = email.Value,
                IsAnonymous = !personalDocument.HasData() && !fullName.HasData(),
            };

            await repository.CreateAsync(client, cancellationToken);

            var response = ClientResponse.MapFromDomain(client);

            return Result.Ok(response);
        }
    }
}
