using Application.UseCases.Clients.GetAllClients;

using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate;
using Domain.UseCases.Clients.Common.Responses;

using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Clients;

public class GetAllClientsUseCaseTests
{
    private readonly GetAllClientsUseCase sut;

    private readonly IClientRepository repository;

    public GetAllClientsUseCaseTests()
    {
        repository = Substitute.For<IClientRepository>();

        sut = new GetAllClientsUseCase(repository);
    }

    [Fact]
    public async Task GetAllClientsSuccessfully()
    {
        // Arrange
        var clients = new List<Client>
        {
            new ClientBuilder()
                .WithSample()
                .Build(),
            new ClientBuilder()
                .WithSample()
                .Build(),
        };

        repository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(clients);

        // Act
        var response = await sut.ExecuteAsync(default);

        // Assert
        response.Should().BeSuccess().And.Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(ClientResponse.MapFromDomain(clients));
        });

        await repository
            .Received(1)
            .GetAllAsync(Arg.Any<CancellationToken>());
    }
}
