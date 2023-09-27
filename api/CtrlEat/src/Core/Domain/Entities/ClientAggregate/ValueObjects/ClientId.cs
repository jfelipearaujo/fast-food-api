using Domain.Common.Models;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public sealed class ClientId : ValueObject
{
    public Guid Value { get; private set; }

    private ClientId(Guid value)
    {
        Value = value;
    }

    public static ClientId CreateUnique()
    {
        return new ClientId(Guid.NewGuid());
    }

    public static ClientId Create(Guid value)
    {
        return new ClientId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
