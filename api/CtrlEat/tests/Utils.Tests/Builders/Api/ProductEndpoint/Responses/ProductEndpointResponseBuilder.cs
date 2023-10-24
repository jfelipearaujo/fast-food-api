using Web.Api.Endpoints.ProductCategories.Responses;
using Web.Api.Endpoints.Products.Requests;
using Web.Api.Endpoints.Products.Responses;

namespace Utils.Tests.Builders.Api.ProductEndpoint.Responses;

public class ProductEndpointResponseBuilder
{
    private string description;
    private decimal amount;
    private string currency;
    private string imageUrl;
    private ProductCategoryEndpointResponse productCategory;

    public ProductEndpointResponseBuilder()
    {
        Reset();
    }

    public ProductEndpointResponseBuilder Reset()
    {
        description = default;
        amount = default;
        currency = default;
        imageUrl = default;
        productCategory = default;
        return this;
    }

    public ProductEndpointResponseBuilder FromRequest(CreateProductEndpointRequest request)
    {
        WithDescription(request.Description);
        WithAmount(request.Amount);
        WithCurrency(request.Currency);
        return this;
    }

    public ProductEndpointResponseBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public ProductEndpointResponseBuilder WithAmount(decimal amount)
    {
        this.amount = amount;
        return this;
    }

    public ProductEndpointResponseBuilder WithCurrency(string currency)
    {
        this.currency = currency;
        return this;
    }

    public ProductEndpointResponseBuilder WithImageUrl(string imageUrl)
    {
        this.imageUrl = imageUrl;
        return this;
    }

    public ProductEndpointResponseBuilder WithProductCategory(ProductCategoryEndpointResponse productCategory)
    {
        this.productCategory = productCategory;
        return this;
    }

    public ProductEndpointResponse Build()
    {
        return new()
        {
            Description = description,
            Amount = amount,
            Currency = currency,
            ImageUrl = imageUrl,
            ProductCategory = productCategory
        };
    }
}
