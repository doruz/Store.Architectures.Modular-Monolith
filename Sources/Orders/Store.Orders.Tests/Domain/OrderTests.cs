using Store.Orders.Contracts;
using Store.Orders.Domain;
using Store.Shared;

namespace Store.Orders.Tests.Domain;

public class OrderTests
{
    private static readonly string CustomerId = Guid.NewGuid().ToString();

    private static readonly OrderLine[] OrderLines =
    [
        new OrderLine(OrderProducts.First.Id, OrderProducts.First.Name, OrderProducts.First.Price.Value, 4),
        new OrderLine(OrderProducts.Second.Id, OrderProducts.Second.Name, OrderProducts.Second.Price.Value, 3),
    ];

    [Fact]
    public void When_OrderIsCreatedWithEmptyCustomerId_Should_ThrowException()
    {
        // Arrange & Act
        var action = () => new Order(string.Empty, OrderLines);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderIsCreatedWithNoLines_Should_ThrowException()
    {
        // Arrange & Act
        var action = () => new Order(CustomerId, []);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderIsCreated_Should_ContainProvidedValues()
    {
        // Arrange & Act
        var systemUnderTest = new Order(CustomerId, OrderLines);

        // Assert
        systemUnderTest.CustomerId.Should().Be(CustomerId);
        systemUnderTest.Lines.Should().BeEquivalentTo(OrderLines);
    }

    [Fact]
    public void When_OrderIsCreated_Should_ContainCorrectNumberOfProducts()
    {
        // Arrange & Act
        var systemUnderTest = new Order(CustomerId, OrderLines);

        // Assert
        systemUnderTest.TotalProducts.Should().Be(7);
    }

    [Fact]
    public void When_OrderIsCreated_Should_ContainCorrectTotalPrice()
    {
        // Arrange & Act
        var systemUnderTest = new Order(CustomerId, OrderLines);

        // Assert
        systemUnderTest.TotalPrice.Should().Be(new Price(4.97m));
    }

    [Fact]
    public void When_OrderIsCreated_Should_CreateCorrectNewOrderEvent()
    {
        // Arrange
        var systemUnderTest = new Order(CustomerId, OrderLines);

        // Act
        var result = systemUnderTest.NewOrderEvent();

        // Assert
        result.Should().BeEquivalentTo(new NewOrderEvent(CustomerId, systemUnderTest.Id)
        {
            Products =
            [
                new NewOrderEvent.Product(OrderProducts.First.Id, 4),
                new NewOrderEvent.Product(OrderProducts.Second.Id, 3)
            ]
        });
    }
}