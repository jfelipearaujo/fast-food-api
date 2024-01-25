using Domain.Entities.ClientAggregate.Enums;

using Persistence;

using System.Net.Http.Json;

using Utils.Tests.Builders.Api.ClientEndpoint.Requests;
using Utils.Tests.Builders.Api.ClientEndpoint.Responses;

using Web.Api.Endpoints;
using Web.Api.Endpoints.Clients;
using Web.Api.Markers;

namespace Contract.Tests.Endpoints.ClientEndpoints;

public class CreateClientTests : IClassFixture<ApiFactory<IApiMarker, AppDbContext>>, IAsyncLifetime
{
    private readonly ApiFactory<IApiMarker, AppDbContext> apiFactory;

    public CreateClientTests(ApiFactory<IApiMarker, AppDbContext> apiFactory)
    {
        this.apiFactory = apiFactory;
    }

    public async Task InitializeAsync()
    {
        await apiFactory.ResetDatabase(apiFactory.Services);
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }

    [Fact]
    public async Task ShouldCreateClientSuccessfully()
    {
        // Arrange
        var httpClient = apiFactory.CreateClient();

        var route = ApiEndpoints.Clients.BaseRoute
            .ReplaceWithVersion(ApiEndpoints.Clients.V1.Version);

        var request = new CreateClientEndpointRequestBuilder()
            .WithSample()
            .Build();

        var expectedResult = new ClientEndpointResponseBuilder()
            .FromRequest(request)
            .WithDocumentType(DocumentType.CPF)
            .WithIsAnonymous(false)
            .Build();

        // Act
        var response = await httpClient.PostAsJsonAsync(route, request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ClientEndpointResponse>();

        result.Should().NotBeNull();
        result.Should()
            .BeEquivalentTo(expectedResult, opt =>
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.CreatedAtUtc)
                    .Excluding(x => x.UpdatedAtUtc));
        result.Id.Should().NotBeEmpty();
        result.CreatedAtUtc.Should().NotBe(DateTime.MinValue);
        result.UpdatedAtUtc.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public async Task ShouldCreateAnAnonymousClientSuccessfully()
    {
        // Arrange
        var httpClient = apiFactory.CreateClient();

        var route = ApiEndpoints.Clients.BaseRoute
            .ReplaceWithVersion(ApiEndpoints.Clients.V1.Version);

        var request = new CreateClientEndpointRequestBuilder()
            .WithEmptySample()
            .Build();

        var expectedResult = new ClientEndpointResponseBuilder()
            .FromRequest(request)
            .WithDocumentType(DocumentType.None)
            .WithIsAnonymous(true)
            .Build();

        // Act
        var response = await httpClient.PostAsJsonAsync(route, request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ClientEndpointResponse>();

        result.Should().NotBeNull();
        result.Should()
            .BeEquivalentTo(expectedResult, opt =>
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.CreatedAtUtc)
                    .Excluding(x => x.UpdatedAtUtc));
        result.Id.Should().NotBeEmpty();
        result.CreatedAtUtc.Should().NotBe(DateTime.MinValue);
        result.UpdatedAtUtc.Should().NotBe(DateTime.MinValue);
    }
}
