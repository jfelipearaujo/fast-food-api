using Domain.Common.Models;

using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities.OrderAggregate.ValueObjects;

public class TrackId : ValueObject
{
    public string Value { get; private set; }

    private TrackId()
    {
    }

    private TrackId(string value)
    {
        Value = value;
    }

    public static TrackId CreateUnique()
    {
        var id = new StringBuilder();

        id.Append((char)RandomNumberGenerator.GetInt32(65, 91));
        id.Append((char)RandomNumberGenerator.GetInt32(65, 91));
        id.Append(RandomNumberGenerator.GetInt32(1000, 9999));

        return new TrackId(id.ToString());
    }

    public static TrackId Create(string value)
    {
        return new TrackId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
