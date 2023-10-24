using System.Net.Http.Json;

using Utils.Tests.Builders.Api.ProductCategoryEndpoint.Requests;
using Utils.Tests.Builders.Api.ProductCategoryEndpoint.Responses;

using Web.Api.Endpoints;
using Web.Api.Endpoints.ProductCategories.Responses;

namespace Contract.Tests.Endpoints.ProductCategoryEndpoints;

[Collection("ContractTests")]
public class CreateProductCategoryTests : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly ApiFactory apiFactory;

    public CreateProductCategoryTests(ApiFactory apiFactory)
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
    public async Task ShouldCreateProductCategorySuccessfully()
    {
        // Arrange
        var httpClient = apiFactory.CreateClient();

        var baseRoute = ApiEndpoints.ProductCategories.BaseRoute
            .ReplaceWithVersion(ApiEndpoints.ProductCategories.V1.Version);

        var request = new CreateProductCategoryEndpointRequestBuilder()
            .WithSample()
            .Build();

        var expectedResult = new ProductCategoryEndpointResponseBuilder()
            .FromRequest(request)
            .Build();

        // Act
        var response = await httpClient.PostAsJsonAsync(baseRoute, request);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProductCategoryEndpointResponse>();

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
