using Domain.Entities.OrderAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.ValueObjects;

public class TrackIdBuilder
{
    private string value;

    public TrackIdBuilder()
    {
        Reset();
    }

    public TrackIdBuilder Reset()
    {
        value = default;

        return this;
    }

    public TrackIdBuilder WithSample()
    {
        value = TrackId.CreateUnique().Value;
        return this;
    }

    public TrackIdBuilder WithValue(string value)
    {
        this.value = value;
        return this;
    }

    public TrackId Build()
    {
        return TrackId.Create(value);
    }
}
