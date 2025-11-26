namespace Store.Shared.Tests.Models;

public class DateTimeModelTests
{
    [Fact]
    public void When_DateIsFormatted_Should_ReturnCorrectDateTimeString()
    {
        // Arrange
        var date = new DateTime(2025, 11, 4, 09, 30, 55);

        // Act
        var result = DateTimeModel.Create(date);

        // Act & Assert
        result.Value.Should().Be(date);
        result.Label.Should().Be("04 Nov 2025, 09:30");
    }
}