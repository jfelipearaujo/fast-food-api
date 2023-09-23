using Application.UseCases.Clients;

using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;
using Domain.Errors.Clients;
using Domain.UseCases.Clients.Requests;

namespace Application.Tests.UseCases.Clients
{
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
                result.Value.Should().BeEquivalentTo(client);
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
}
