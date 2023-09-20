using Application.UseCases.Clients;

using Domain.Adapters;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;

namespace Application.Tests.UseCases.Clients
{
    public class CreateClientUseCaseTests
    {
        private readonly CreateClientUseCase sut;

        private readonly IClientRepository repository;

        public CreateClientUseCaseTests()
        {
            repository = Substitute.For<IClientRepository>();

            sut = new CreateClientUseCase(repository);
        }

        [Fact]
        public async Task ShouldCreateClientWithPersonalDataSuccessfully()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentId = "46808459029",
            };

            var expectedResponse = new ClientResponse
            {
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
                result.Value.Id.Should().NotBeEmpty();
            });

            await repository
                .Received(1)
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldCreateClientWithOnlyDocumentIdSuccessfully()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                DocumentId = "46808459029",
            };

            var expectedResponse = new ClientResponse
            {
                FirstName = null,
                LastName = null,
                Email = null,
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
                result.Value.Id.Should().NotBeEmpty();
            });

            await repository
                .Received(1)
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldCreateClientWithoutAnyPersonalDataSuccessfully()
        {
            // Arrange
            var request = new CreateClientRequest();

            var expectedResponse = new ClientResponse
            {
                FirstName = null,
                LastName = null,
                Email = null,
                DocumentType = DocumentType.None,
                DocumentId = null,
                IsAnonymous = true,
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeSuccess().And.Satisfy(result =>
            {
                result.Value.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(x => x.Id));
                result.Value.Id.Should().NotBeEmpty();
            });

            await repository
                .Received(1)
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleMissingFirstName()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentId = "46808459029",
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ClientRegistrationWithoutFirstNameError());

            await repository
                .DidNotReceive()
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleMissingEmail()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                FirstName = "João",
                LastName = "Silva",
                DocumentId = "46808459029",
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ClientRegistrationWithoutEmailError());

            await repository
                .DidNotReceive()
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleInvalidDocumentId()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentId = "1234",
            };

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ClientRegistrationInvalidDocumentIdError());

            await repository
                .DidNotReceive()
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleAlreadyExistEmail()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentId = "46808459029",
            };

            repository
                .GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new Client());

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ClientRegistrationEmailAlreadyExistsError());

            await repository
                .DidNotReceive()
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task ShouldHandleAlreadyExistDocumentId()
        {
            // Arrange
            var request = new CreateClientRequest
            {
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentId = "46808459029",
            };

            repository
                .GetByDocumentIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new Client());

            // Act
            var response = await sut.ExecuteAsync(request, cancellationToken: default);

            // Assert
            response.Should().BeFailure().And.HaveReason(new ClientRegistrationDocumentIdAlreadyExistsError());

            await repository
                .DidNotReceive()
                .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
        }
    }
}
