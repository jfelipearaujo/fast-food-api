using Web.Api.Endpoints.ProductCategories.Requests;

namespace Utils.Tests.Builders.Api.ProductCategoryEndpoint.Requests;

public class CreateProductCategoryEndpointRequestBuilder
{
    private string description;

    public CreateProductCategoryEndpointRequestBuilder()
    {
        Reset();
    }

    public CreateProductCategoryEndpointRequestBuilder Reset()
    {
        description = default;
        return this;
    }

    public CreateProductCategoryEndpointRequestBuilder WithSample()
    {
        WithDescription("Product Category");
        return this;
    }

    public CreateProductCategoryEndpointRequestBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public CreateProductCategoryEndpointRequest Build()
    {
        return new CreateProductCategoryEndpointRequest
        {
            Description = description
        };
    }
}
