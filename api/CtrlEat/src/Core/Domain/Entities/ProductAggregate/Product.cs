using Domain.Common.Models;
using Domain.Entities.OrderAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    public string Description { get; private set; }

    public Money Price { get; private set; }

    public string ImageUrl { get; private set; }

    public ProductCategoryId ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public Stock Stock { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    [ExcludeFromCodeCoverage]
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
        string? description = null,
        Money? price = null,
        string? imageUrl = null)
    {
        Description = description ?? Description;
        Price = price ?? Price;
        ImageUrl = imageUrl ?? ImageUrl;
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
