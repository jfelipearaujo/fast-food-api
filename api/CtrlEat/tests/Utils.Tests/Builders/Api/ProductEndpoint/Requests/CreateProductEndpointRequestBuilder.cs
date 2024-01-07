using Web.Api.Endpoints.Products;

namespace Utils.Tests.Builders.Api.ProductEndpoint.Requests;

public class CreateProductEndpointRequestBuilder
{
    private Guid productCategoryId;
    private string description;
    private decimal amount;
    private string currency;

    public CreateProductEndpointRequestBuilder()
    {
        Reset();
    }

    public CreateProductEndpointRequestBuilder Reset()
    {
        productCategoryId = default;
        description = default;
        amount = default;
        currency = default;
        return this;
    }

    public CreateProductEndpointRequestBuilder WithSample()
    {
        WithProductCategoryId(Guid.NewGuid());
        WithDescription("Product description");
        WithAmount(1.15m);
        WithCurrency("BRL");
        return this;
    }

    public CreateProductEndpointRequestBuilder WithProductCategoryId(Guid productCategoryId)
    {
        this.productCategoryId = productCategoryId;
        return this;
    }

    public CreateProductEndpointRequestBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public CreateProductEndpointRequestBuilder WithAmount(decimal amount)
    {
        this.amount = amount;
        return this;
    }

    public CreateProductEndpointRequestBuilder WithCurrency(string currency)
    {
        this.currency = currency;
        return this;
    }

    public CreateProductEndpointRequest Build()
    {
        return new CreateProductEndpointRequest
        {
            ProductCategoryId = productCategoryId,
            Description = description,
            Amount = amount,
            Currency = currency,
        };
    }
}
