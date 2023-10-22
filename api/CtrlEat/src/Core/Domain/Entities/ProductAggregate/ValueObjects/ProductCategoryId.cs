using Domain.Common.Models;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ProductAggregate.ValueObjects;

public sealed class ProductCategoryId : ValueObject
{
    public Guid Value { get; private set; }

    [ExcludeFromCodeCoverage]
    private ProductCategoryId()
    {
    }

    private ProductCategoryId(Guid value)
    {
        Value = value;
    }

    public static ProductCategoryId CreateUnique()
    {
        return new ProductCategoryId(Guid.NewGuid());
    }

    public static ProductCategoryId Create(Guid value)
    {
        return new ProductCategoryId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
