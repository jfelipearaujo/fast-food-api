using Web.Api.Endpoints.ProductCategories.Requests;
using Web.Api.Endpoints.ProductCategories.Responses;

namespace Utils.Tests.Builders.Api.ProductCategoryEndpoint.Responses;

public class ProductCategoryEndpointResponseBuilder
{
    private string description;

    public ProductCategoryEndpointResponseBuilder()
    {
        Reset();
    }

    public ProductCategoryEndpointResponseBuilder Reset()
    {
        description = default;
        return this;
    }

    public ProductCategoryEndpointResponseBuilder FromRequest(CreateProductCategoryEndpointRequest request)
    {
        WithDescription(request.Description);
        return this;
    }

    public ProductCategoryEndpointResponseBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public ProductCategoryEndpointResponse Build()
    {
        return new ProductCategoryEndpointResponse
        {
            Description = description
        };
    }
}
