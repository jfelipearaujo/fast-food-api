using Domain.Common.Models;

namespace Domain.Entities.ProductAggregate.ValueObjects;

public sealed class ProductCategoryId : ValueObject
{
    public Guid Value { get; private set; }

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
