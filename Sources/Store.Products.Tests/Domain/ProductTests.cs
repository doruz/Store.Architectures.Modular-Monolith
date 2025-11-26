using Store.Products.Domain;
using Store.Shared;

namespace Store.Products.Tests.Domain;

public class ProductTests
{
    private readonly Product _systemUnderTest = new("test", 1.0m, 10);

    [Fact]
    public void When_AllProductDetailsAreUpdated_Should_OverwriteAll()
    {
        // Act
        _systemUnderTest.Update("updated", 1.75m, 20);

        // Assert
        _systemUnderTest.Name.Should().Be("updated");
        _systemUnderTest.Price.Should().Be(new Price(1.75m));
        _systemUnderTest.Stock.Should().Be(20);
    }

    [Fact]
    public void When_OnlyNameIsUpdated_Should_OverwriteTheName()
    {
        // Act
        _systemUnderTest.Update("new name", null, null);

        // Assert
        _systemUnderTest.Name.Should().Be("new name");
        _systemUnderTest.Price.Should().Be(new Price(1.00m));
        _systemUnderTest.Stock.Should().Be(10);
    }

    [Fact]
    public void When_OnlyPriceIsUpdated_Should_OverwriteThePrice()
    {
        // Act
        _systemUnderTest.Update(null, 1.25m, null);

        // Assert
        _systemUnderTest.Name.Should().Be("test");
        _systemUnderTest.Price.Should().Be(new Price(1.25m));
        _systemUnderTest.Stock.Should().Be(10);
    }

    [Fact]
    public void When_OnlyStockIsUpdated_Should_OverwriteTheStock()
    {
        // Act
        _systemUnderTest.Update(null, null, 5);

        // Assert
        _systemUnderTest.Name.Should().Be("test");
        _systemUnderTest.Price.Should().Be(new Price(1.0m));
        _systemUnderTest.Stock.Should().Be(5);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(2, 8)]
    [InlineData(5, 5)]
    [InlineData(10, 0)]
    [InlineData(11, -1)]
    public void When_StockIsDecreased_Should_BeUpdated(int quantity, int expectedStock)
    {
        // Act
        _systemUnderTest.DecreaseStock(quantity);

        // Assert
        _systemUnderTest.Stock.Should().Be(expectedStock);
    }
}