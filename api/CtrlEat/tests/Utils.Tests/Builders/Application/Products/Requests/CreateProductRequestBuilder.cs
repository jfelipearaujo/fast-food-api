using Domain.UseCases.Products.CreateProduct.Requests;

namespace Utils.Tests.Builders.Application.Products.Requests;

public class CreateProductRequestBuilder
{
    private Guid productCategoryId;
    private string description;
    private string currency;
    private decimal amount;
    private string imageUrl;

    public CreateProductRequestBuilder()
    {
        Reset();
    }

    public CreateProductRequestBuilder Reset()
    {
        productCategoryId = default;
        description = default;
        currency = default;
        amount = default;
        imageUrl = default;

        return this;
    }

    public CreateProductRequestBuilder WithSample()
    {
        WithProductCategoryId(Guid.NewGuid());
        WithDescription("Product");
        WithCurrency("BRL");
        WithAmount(1.0m);
        WithImageUrl("http://image.com/123.png");

        return this;
    }

    public CreateProductRequestBuilder WithProductCategoryId(Guid productCategoryId)
    {
        this.productCategoryId = productCategoryId;
        return this;
    }

    public CreateProductRequestBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public CreateProductRequestBuilder WithCurrency(string currency)
    {
        this.currency = currency;
        return this;
    }

    public CreateProductRequestBuilder WithAmount(decimal amount)
    {
        this.amount = amount;
        return this;
    }

    public CreateProductRequestBuilder WithImageUrl(string imageUrl)
    {
        this.imageUrl = imageUrl;
        return this;
    }

    public CreateProductRequest Build()
    {
        return new()
        {
            ProductCategoryId = productCategoryId,
            Description = description,
            Currency = currency,
            Amount = amount,
        };
    }
}
