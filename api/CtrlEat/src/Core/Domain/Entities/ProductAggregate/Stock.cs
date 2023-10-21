using Domain.Common.Models;
using Domain.Entities.ProductAggregate.Errors;
using Domain.Entities.ProductAggregate.ValueObjects;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ProductAggregate;

public sealed class Stock : AggregateRoot<StockId>
{
    public int Quantity { get; private set; }

    public ProductId ProductId { get; set; }
    public Product Product { get; set; }

    [ExcludeFromCodeCoverage]
    private Stock()
    {
    }

    private Stock(int quantity,
        Product product,
        StockId? stockId = null)
        : base(stockId ?? StockId.CreateUnique())
    {
        Product = product;
        Quantity = quantity;
    }

    public static Result<Stock> Create(int quantity,
        Product product,
        StockId? stockId = null)
    {
        if (quantity <= 0)
        {
            return Result.Fail(new StockInvalidQuantityError());
        }

        return new Stock(quantity, product, stockId);
    }
}
