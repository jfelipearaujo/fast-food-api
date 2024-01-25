using Domain.Common.Models;
using Domain.Entities.OrderAggregate.Errors;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentResults;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Domain.Entities.OrderAggregate;

public sealed class OrderItem : AggregateRoot<OrderItemId>
{
    public const int MIN_QUANTITY = 0;

    public int Quantity { get; private set; }

    public string Observation { get; private set; }

    public string Description { get; private set; }

    public Money Price { get; private set; }

    public string ImageUrl { get; private set; }

    public OrderId OrderId { get; set; }
    public Order Order { get; set; }

    public ProductId ProductId { get; set; }
    public Product Product { get; set; }

    [ExcludeFromCodeCoverage]
    private OrderItem()
    {
    }

    public OrderItem(
        int quantity,
        string observation,
        string description,
        Money price,
        string imageUrl,
        Product product,
        OrderItemId? orderItemId = null)
        : base(orderItemId ?? OrderItemId.CreateUnique())
    {
        Quantity = quantity;
        Observation = observation;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        Product = product;
        ProductId = product.Id;
    }

    public string FormatPrice()
    {
        var currencyCultureName = Price.Currency == Money.BRL ? "pt-BR" : "en-US";

        return Price.Amount.ToString("C", CultureInfo.CreateSpecificCulture(currencyCultureName));
    }

    public static Result<OrderItem> Create(
        int quantity,
        string observation,
        Product product,
        OrderItemId? orderItemId = null)
    {
        if (quantity <= MIN_QUANTITY)
        {
            return Result.Fail(new OrderItemInvalidQuantityError());
        }

        return new OrderItem(
            quantity,
            observation,
            product.Description,
            product.Price,
            product.ImageUrl,
            product,
            orderItemId);
    }
}