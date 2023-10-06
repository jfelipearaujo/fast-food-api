using Domain.Common.Models;

namespace Domain.Entities.OrderAggregate.ValueObjects;

public sealed class OrderItemId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderItemId()
    {
    }

    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId CreateUnique()
    {
        return new OrderItemId(Guid.NewGuid());
    }

    public static OrderItemId Create(Guid value)
    {
        return new OrderItemId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
