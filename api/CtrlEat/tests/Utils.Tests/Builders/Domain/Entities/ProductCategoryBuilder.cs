using Domain.Entities.ProductCategoryAggregate;
using Domain.Entities.ProductCategoryAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

public class ProductCategoryBuilder
{
    private ProductCategoryId id;
    private string description;

    public ProductCategoryBuilder()
    {
        Reset();
    }

    public ProductCategoryBuilder Reset()
    {
        id = default;
        description = default;

        return this;
    }

    public ProductCategoryBuilder WithSample()
    {
        id = ProductCategoryId.CreateUnique();
        description = "Product Category";

        return this;
    }

    public ProductCategoryBuilder WithId(Guid id)
    {
        this.id = ProductCategoryId.Create(id);

        return this;
    }

    public ProductCategoryBuilder WithDescription(string description)
    {
        this.description = description;

        return this;
    }

    public ProductCategory Build()
    {
        return ProductCategory.Create(
            description,
            id
        ).ValueOrDefault;
    }
}