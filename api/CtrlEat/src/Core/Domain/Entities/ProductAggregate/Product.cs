using Domain.Common.Models;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.Entities.ProductCategoryAggregate;
using Domain.Entities.ProductCategoryAggregate.ValueObjects;

using FluentResults;

namespace Domain.Entities.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    public string Description { get; private set; }

    public Money Price { get; private set; }

    public string ImageUrl { get; private set; }

    public ProductCategoryId ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    private Product()
    {

    }

    private Product(
        string description,
        Money price,
        string imageUrl,
        ProductCategory productCategory,
        ProductId? productId = null)
        : base(productId ?? ProductId.CreateUnique())
    {
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        ProductCategory = productCategory;
        ProductCategoryId = productCategory.Id;
    }

    public void Update(
        string description,
        Money price,
        string imageUrl)
    {
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }

    public static Result<Product> Create(
        string description,
        Money price,
        string imageUrl,
        ProductCategory productCategory,
        ProductId? productId = null)
    {
        return new Product(description, price, imageUrl, productCategory, productId);
    }
}
