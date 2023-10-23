using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using Utils.Tests.Builders.Domain.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class ProductBuilder
{
    private ProductId id;
    private string description;
    private Money price;
    private ProductCategory productCategory;

    public ProductBuilder()
    {
        Reset();
    }

    public ProductBuilder Reset()
    {
        id = default;
        description = default;
        price = default;
        productCategory = default;

        return this;
    }

    public ProductBuilder WithSample()
    {
        id = ProductId.CreateUnique();
        description = "Product Description";
        price = new MoneyBuilder().WithSample().Build();

        return this;
    }

    public ProductBuilder WithId(Guid id)
    {
        this.id = ProductId.Create(id);

        return this;
    }

    public ProductBuilder WithProductCategory(ProductCategory productCategory)
    {
        this.productCategory = productCategory;

        return this;
    }

    public ProductBuilder WithDescription(string description)
    {
        this.description = description;

        return this;
    }

    public ProductBuilder WithPrice(string currency, decimal amount)
    {
        price = new MoneyBuilder()
            .WithCurrency(currency)
            .WithAmount(amount)
            .Build();

        return this;
    }

    public Product Build()
    {
        return Product.Create(
            description,
            price,
            productCategory,
            id
        ).Value;
    }
}