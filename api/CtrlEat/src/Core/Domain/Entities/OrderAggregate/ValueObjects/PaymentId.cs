using Domain.Common.Models;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.OrderAggregate.ValueObjects;

public sealed class PaymentId : ValueObject
{
    public Guid Value { get; private set; }

    [ExcludeFromCodeCoverage]
    private PaymentId()
    {
    }

    private PaymentId(Guid value)
    {
        Value = value;
    }

    public static PaymentId CreateUnique()
    {
        return new PaymentId(Guid.NewGuid());
    }

    public static PaymentId Create(Guid value)
    {
        return new PaymentId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}