using Domain.UseCases.Products.UpdateProduct.Requests;

namespace Utils.Tests.Builders.Application.Products.Requests;

public class UpdateProductRequestBuilder
{
    private Guid productId;
    private Guid productCategoryId;
    private string description;
    private decimal amount;
    private string currency;
    private string imageUrl;

    public UpdateProductRequestBuilder()
    {
        Reset();
    }

    public UpdateProductRequestBuilder Reset()
    {
        productId = default;
        productCategoryId = default;
        description = default;
        amount = default;
        currency = default;
        imageUrl = default;

        return this;
    }

    public UpdateProductRequestBuilder WithSample()
    {
        WithProductId(Guid.NewGuid());
        WithProductCategoryId(Guid.NewGuid());
        WithDescription("Product Description");
        WithAmount(10);
        WithCurrency("BRL");
        WithImageUrl("http://image.com/123.png");

        return this;
    }

    public UpdateProductRequestBuilder WithProductId(Guid productId)
    {
        this.productId = productId;
        return this;
    }

    public UpdateProductRequestBuilder WithProductCategoryId(Guid productCategoryId)
    {
        this.productCategoryId = productCategoryId;
        return this;
    }

    public UpdateProductRequestBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public UpdateProductRequestBuilder WithAmount(decimal amount)
    {
        this.amount = amount;
        return this;
    }

    public UpdateProductRequestBuilder WithCurrency(string currency)
    {
        this.currency = currency;
        return this;
    }

    public UpdateProductRequestBuilder WithImageUrl(string imageUrl)
    {
        this.imageUrl = imageUrl;
        return this;
    }

    public UpdateProductRequest Build()
    {
        return new()
        {
            ProductId = productId,
            ProductCategoryId = productCategoryId,
            Description = description,
            Amount = amount,
            Currency = currency,
            ImageUrl = imageUrl
        };
    }
}
