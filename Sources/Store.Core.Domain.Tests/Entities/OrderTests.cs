using FluentAssertions;
using Store.Orders.Domain;
using Store.Shared;

namespace Store.Core.Domain.Tests.Entities;

public class OrderTests
{
    private static readonly string CustomerId = Guid.NewGuid().ToString();

    private static readonly OrderLine[] OrderLines = 
    [
        new OrderLine(Products.First.Id, Products.First.Name, Products.First.Price, 4),
        new OrderLine(Products.Second.Id, Products.Second.Name, Products.Second.Price, 3),
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
}