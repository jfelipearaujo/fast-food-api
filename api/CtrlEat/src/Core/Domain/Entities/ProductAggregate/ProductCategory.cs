using Domain.Common.Models;
using Domain.Entities.ProductAggregate.Errors;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentResults;

namespace Domain.Entities.ProductAggregate;

public sealed class ProductCategory : AggregateRoot<ProductCategoryId>
{
    private const int MAX_DESCRIPTION_LENGTH = 250;

    public string Description { get; private set; }

    public ICollection<Product> Products { get; set; }

    private ProductCategory()
    {
    }

    private ProductCategory(
        string description,
        ProductCategoryId? productCategoryId = null)
        : base(productCategoryId ?? ProductCategoryId.CreateUnique())
    {
        Description = description;
    }

    public void Update(string description)
    {
        Description = description;
    }

    public static Result<ProductCategory> Create(
        string description,
        ProductCategoryId? productCategoryId = null)
    {

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Fail(new ProductCategoryDescriptionInvalidError());
        }

        if (description.Length > MAX_DESCRIPTION_LENGTH)
        {
            return Result.Fail(new ProductCategoryDescriptionMaxLengthError(MAX_DESCRIPTION_LENGTH));
        }

        return new ProductCategory(description, productCategoryId);
    }
}