using Application.UseCases.Clients.CreateClient;
using Application.UseCases.Clients.CreateClient.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.Enums;
using Domain.Entities.ClientAggregate.ValueObjects;

using Utils.Tests.Builders.Application.Clients.Requests;
using Utils.Tests.Builders.Application.Clients.Responses;
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
        var request = new CreateClientRequestBuilder()
            .WithSample()
            .Build();

        var expectedResponse = new ClientResponseBuilder()
            .WithSample()
            .Build();

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
        var request = new CreateClientRequestBuilder()
            .WithDocumentId("46808459029")
            .Build();

        var expectedResponse = new ClientResponseBuilder()
            .WithDocumentId("46808459029")
            .WithDocumentType(DocumentType.CPF)
            .Build();

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
        var request = new CreateClientRequestBuilder()
            .Build();

        var expectedResponse = new ClientResponseBuilder()
            .WithDocumentType(DocumentType.None)
            .WithIsAnonymous(true)
            .Build();

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
    public async Task ShouldHandleWhenSomePersonalInformationIsInvalid()
    {
        // Arrange
        var email = "joao.silva@email";

        var request = new CreateClientRequestBuilder()
            .WithSample()
            .WithEmail(email)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure();

        await repository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleWhenNameIsProvidedButNotEmail()
    {
        // Arrange
        var request = new CreateClientRequestBuilder()
            .WithSample()
            .WithEmail(string.Empty)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ClientRegistrationWithoutEmailError());

        await repository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldHandleAlreadyExistEmail()
    {
        // Arrange
        var email = "joao.silva@email.com";

        var request = new CreateClientRequestBuilder()
            .WithSample()
            .WithEmail(email)
            .Build();

        var client = new ClientBuilder()
            .WithSample()
            .WithEmail(email)
            .Build();

        repository
            .GetByEmailAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns(client);

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
        var documentId = "46808459029";

        var request = new CreateClientRequestBuilder()
            .WithSample()
            .WithDocumentId(documentId)
            .Build();

        var client = new ClientBuilder()
            .WithSample()
            .WithDocumentId(documentId)
            .Build();

        repository
            .GetByDocumentIdAsync(Arg.Any<DocumentId>(), Arg.Any<CancellationToken>())
            .Returns(client);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ClientRegistrationDocumentIdAlreadyExistsError());

        await repository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
    }
}
