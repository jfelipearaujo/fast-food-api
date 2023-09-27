using Application.UseCases.Clients.GetClientById;
using Application.UseCases.Clients.GetClientById.Errors;

using Domain.Adapters;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Clients;

public class GetClientByIdUseCaseTests
{
    private readonly GetClientByIdUseCase sut;

    private readonly IClientRepository repository;

    public GetClientByIdUseCaseTests()
    {
        repository = Substitute.For<IClientRepository>();

        sut = new GetClientByIdUseCase(repository);
    }

    [Fact]
    public async Task ShouldGetClientByIdSuccessfully()
    {
        // Arrange
        var request = new GetClientByIdRequest
        {
            Id = Guid.NewGuid(),
        };

        var client = new ClientBuilder()
            .WithSample()
            .WithId(request.Id)
            .Build();

        repository
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>())
            .Returns(client);

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(ClientResponse.MapFromDomain(client));
        });
    }

    [Fact]
    public async Task ShouldHandleWhenFindNothing()
    {
        // Arrange
        var request = new GetClientByIdRequest
        {
            Id = Guid.NewGuid(),
        };

        repository
            .GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>())
            .Returns(default(Client));

        // Act
        var response = await sut.ExecuteAsync(request, cancellationToken: default);

        // Assert
        response.Should().BeFailure().And.HaveReason(new ClientNotFoundError(request.Id));
    }
}
