using Domain.Entities.OrderAggregate.ValueObjects;

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

    // create a test to validate a already created track id
    [Fact]
    public void ShouldCreateValidTrackId()
    {
        // Arrange
        var trackId = "AA1234";

        var expected = TrackId.Create(trackId);

        // Act
        var result = TrackId.Create(trackId);

        // Assert
        result.Should().NotBeNull();
        result.Equals(expected).Should().BeTrue();
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
