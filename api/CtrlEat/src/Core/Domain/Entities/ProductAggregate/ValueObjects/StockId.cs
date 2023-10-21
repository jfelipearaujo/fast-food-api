using Domain.Common.Models;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ProductAggregate.ValueObjects;

public sealed class StockId : ValueObject
{
    public Guid Value { get; private set; }

    [ExcludeFromCodeCoverage]
    private StockId()
    {
    }

    private StockId(Guid value)
    {
        Value = value;
    }

    public static StockId CreateUnique()
    {
        return new StockId(Guid.NewGuid());
    }

    public static StockId Create(Guid value)
    {
        return new StockId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
