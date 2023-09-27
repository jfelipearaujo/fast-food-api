using Domain.Entities.ClientAggregate.Validators;

using FluentAssertions;

namespace Domain.Tests.Validators;

public class CpfTests
{
    [Theory]
    [InlineData("03740757000", true)]
    [InlineData("03740234700", false)]
    [InlineData("", false)]
    public void ShouldValidateCpf(string cpf, bool expected)
    {
        // Arrange

        // Act
        var result = CpfValidator.Check(cpf);

        // Assert
        result.Should().Be(expected);
    }
}
