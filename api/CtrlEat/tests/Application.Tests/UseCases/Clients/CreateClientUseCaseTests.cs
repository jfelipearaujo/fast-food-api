using Application.UseCases.Clients.CreateClient;
using Application.UseCases.Clients.CreateClient.Errors;

using Domain.Adapters;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.Enums;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Clients.Common.Responses;
using Domain.UseCases.Clients.CreateClient.Requests;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Clients;

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
            FirstName = string.Empty,
            LastName = string.Empty,
            Email = string.Empty,
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
            FirstName = string.Empty,
            LastName = string.Empty,
            Email = string.Empty,
            DocumentType = DocumentType.None,
            DocumentId = string.Empty,
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
            .GetByEmailAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns(new ClientBuilder().WithSample().Build());

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
            .GetByDocumentIdAsync(Arg.Any<DocumentId>(), Arg.Any<CancellationToken>())
            .Returns(new ClientBuilder().WithSample().Build());

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ClientRegistrationDocumentIdAlreadyExistsError());

        await repository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
    }
}
