using Store.Products.Contracts;
using Store.Shared;

namespace Store.Products.Tests.Contracts;

public class ProductModelTests
{
    private readonly ProductModel _systemUnderTest = new()
    {
        Id = "test",
        Name = "Test",
        Price = PriceModel.Create(1.0m),
        Stock = 10
    };

    [Theory]
    [InlineData(-1, false)]
    [InlineData(1, true)]
    [InlineData(9, true)]
    [InlineData(10, true)]
    [InlineData(11, false)]
    public void When_CheckingStockAvailability_Should_ReturnCorrectValue(int quantity, bool expectedAvailability)
    {
        _systemUnderTest
            .IsStockAvailable(quantity)
            .Should().Be(expectedAvailability);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    [InlineData(10)]
    public void When_EnsuringStockIsAvailable_Should_ReturnProduct(int quantity)
    {
        _systemUnderTest
            .EnsureStockIsAvailable(quantity)
            .Should().Be(_systemUnderTest);
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void When_EnsuringStockIsNotAvailable_Should_ThrownAppError(int quantity)
    {
        var action = () => _systemUnderTest.EnsureStockIsAvailable(quantity);

        action.Should().Throw<AppError>()
            .And.Error.Should().Be("product_stock_is_not_available");
    }
}