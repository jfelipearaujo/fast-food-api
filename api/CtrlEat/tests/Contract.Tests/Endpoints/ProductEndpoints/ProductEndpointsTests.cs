using Persistence;

using System.Net.Http.Json;

using Utils.Tests.Builders.Api.ProductCategoryEndpoint.Requests;
using Utils.Tests.Builders.Api.ProductEndpoint.Requests;
using Utils.Tests.Builders.Api.ProductEndpoint.Responses;

using Web.Api.Endpoints;
using Web.Api.Endpoints.ProductCategories;
using Web.Api.Endpoints.Products;
using Web.Api.Markers;

namespace Contract.Tests.Endpoints.ProductEndpoints;

public class ProductEndpointsTests : IClassFixture<ApiFactory<IApiMarker, AppDbContext>>, IAsyncLifetime
{
    private readonly ApiFactory<IApiMarker, AppDbContext> apiFactory;

    public ProductEndpointsTests(ApiFactory<IApiMarker, AppDbContext> apiFactory)
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
    public async Task ShouldCreateProductSuccessfully()
    {
        // Arrange
        var httpClient = apiFactory.CreateClient();

        var productCategoryBaseRoute = ApiEndpoints.ProductCategories.BaseRoute
            .ReplaceWithVersion(ApiEndpoints.ProductCategories.V1.Version);

        var productCategoryRequest = new CreateProductCategoryEndpointRequestBuilder()
            .WithSample()
            .Build();

        var productCategoryResponse = await httpClient.PostAsJsonAsync(productCategoryBaseRoute, productCategoryRequest);

        productCategoryResponse.EnsureSuccessStatusCode();

        var productCategory = await productCategoryResponse.Content.ReadFromJsonAsync<ProductCategoryEndpointResponse>();

        productCategory.Should().NotBeNull();

        // --

        var productBaseRoute = ApiEndpoints.Products.BaseRoute
            .ReplaceWithVersion(ApiEndpoints.Products.V1.Version);

        var productRequest = new CreateProductEndpointRequestBuilder()
            .WithSample()
            .WithProductCategoryId(productCategory.Id)
            .Build();

        var expectedResult = new ProductEndpointResponseBuilder()
            .FromRequest(productRequest)
            .WithImageUrl(string.Empty)
            .WithProductCategory(productCategory)
            .Build();

        // Act
        var response = await httpClient.PostAsJsonAsync(productBaseRoute, productRequest);

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ProductEndpointResponse>();

        result.Should().NotBeNull();
        result.Should()
            .BeEquivalentTo(expectedResult, opt =>
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.CreatedAtUtc)
                    .Excluding(x => x.UpdatedAtUtc)
                    .Excluding(x => x.ProductCategory.CreatedAtUtc)
                    .Excluding(x => x.ProductCategory.UpdatedAtUtc));
        result.Id.Should().NotBeEmpty();
        result.CreatedAtUtc.Should().NotBe(DateTime.MinValue);
        result.UpdatedAtUtc.Should().NotBe(DateTime.MinValue);
    }
}
