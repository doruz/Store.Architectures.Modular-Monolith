using FluentAssertions;
using Store.Orders.Domain;
using Store.Products.Domain;
using Store.Shared;
using Store.ShoppingCarts.Domain;

namespace Store.Core.Domain.Tests.Entities;

public class OrderLineTests
{
    private static readonly Product Product = Products.Second;

    [Fact]
    public void When_OrderLineIsCreated_Should_ThrowExceptionWhenQuantityIsNegative()
    {
        // Arrange & Act
        var action = () =>
        {
            new OrderLine(Product.Id, Product.Name, Product.Price, -1);
        };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_ContainAllDetails()
    {
        // Arrange & Act
        var systemUnderTest = new OrderLine(Product.Id, Product.Name, Product.Price, 3);

        // Assert
        systemUnderTest.ProductId.Should().Be(Product.Id);
        systemUnderTest.ProductName.Should().Be(Product.Name);
        systemUnderTest.ProductPrice.Should().Be(Product.Price);
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_ContainCorrectTotalPrice()
    {
        // Arrange & Act
        var systemUnderTest = new OrderLine(Product.Id, Product.Name, Product.Price, 3);

        // Assert
        systemUnderTest.TotalPrice.Should().Be(new Price(2.97m));
    }
}