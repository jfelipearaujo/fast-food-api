using Domain.Entities.OrderAggregate.ValueObjects;

using FluentAssertions;

namespace Domain.Tests.ValueObjects;

public class TrackIdTests
{
    [Fact]
    public void ShouldGenerateUniqueId()
    {
        // Arrange

        // Act
        var trackId = TrackId.CreateUnique();

        // Assert
        trackId.Should().NotBeNull();
    }

    [Fact]
    public void ShouldGenerate100UniqueIds()
    {
        // Arrange
        var ids = 100;

        // Act
        var trackIds = new List<TrackId>();

        for (int i = 0; i < ids; i++)
        {
            trackIds.Add(TrackId.CreateUnique());
        }

        // Assert
        trackIds.Distinct().Count().Should().Be(ids);
    }
}
