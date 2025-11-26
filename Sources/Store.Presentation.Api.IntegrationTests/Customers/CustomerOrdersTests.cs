using Store.Shared;

namespace Store.Presentation.Api.IntegrationTests.Customers;

public class CustomerOrdersTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_CustomerDoesNotHaveOrders_Should_ReturnSuccessWithEmptyDetails()
    {
        // Arrange
        await Database.DeleteCustomerOrders(CurrentCustomer.Id);

        // Act
        var response = await Api.Customer.Orders.GetAllAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync<OrderSummaryTestModel[]>([]);
    }

    [Fact]
    public async Task When_CustomerHasOrders_Should_ReturnSuccessWithSummariesDetails()
    {
        // Arrange
        await Database.DeleteCustomerOrders(CurrentCustomer.Id);

        var orders = new []
        {
            await CreateOrder(cart => cart.Apples(1).Bananas(2)),
            await CreateOrder(cart => cart.Apples(3).Bananas(1))
        };

        var expectedOrderSummaries = new []
        {
            new OrderSummaryTestModel
            {
                Id = orders[1].Id,
                OrderedAt = orders[1].OrderedAt,
                TotalProducts = 4,
                TotalPrice = new PriceTestModel(3.72m)
            },
            new OrderSummaryTestModel
            {
                Id = orders[0].Id,
                OrderedAt = orders[0].OrderedAt,
                TotalProducts = 3,
                TotalPrice = new PriceTestModel(2.49m)
            }
        };

        // Act
        var response = await Api.Customer.Orders.GetAllAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedOrderSummaries);
    }

    [Fact]
    public async Task When_OrderDoesNotExists_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Orders.FindAsync("unknown_id");

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.NotFound)
            .And.ContainContentAsync(new AppErrorTestModel("not_found")); ;
    }

    [Fact]
    public async Task When_OrderExists_Should_ReturnOrderDetails()
    {
        // Arrange
        var order = await CreateOrder(cart => cart.Apples(2).Bananas(3));

        var expectedOrderDetails = new OrderDetailsTestModel
        {
            Id = order.Id,
            OrderedAt = order.OrderedAt,
            TotalProducts = 5,
            TotalPrice = new PriceTestModel(4.23m),

            Lines = 
            [
                new OrderDetailsLineTestModel
                {
                    ProductId = TestProducts.Apples.Id,
                    ProductName = TestProducts.Apples.Name,
                    ProductPrice = new PriceTestModel(0.99m),

                    Quantity = 2,
                    TotalPrice = new PriceTestModel(1.98m)
                },

                new OrderDetailsLineTestModel
                {
                    ProductId = TestProducts.Bananas.Id,
                    ProductName = TestProducts.Bananas.Name,
                    ProductPrice = new PriceTestModel(0.75m),

                    Quantity = 3,
                    TotalPrice = new PriceTestModel(2.25m)
                }
            ]
        };

        // Act
        var response = await Api.Customer.Orders.FindAsync(order.Id);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedOrderDetails);
    }

    [Fact]
    public async Task When_EmptyOrderIsCreated_Should_ReturnError()
    {
        // Act
        var response = await Api.Customer.Orders.CreateAsync(_ => { });

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.BadRequest)
            .And.ContainContentAsync(new AppErrorTestModel("order_cannot_be_empty"));
    }

    [Fact]
    public async Task When_CreatingOrderWithUnknownProduct_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Orders
            .CreateAsync(cart => cart.With(TestProducts.UnknownId, 1));

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.NotFound)
            .And.ContainContentAsync(new AppErrorTestModel("product_not_found"));
    }

    [Fact]
    public async Task When_CreatingOrderWithUnavailableStock_Should_ReturnConflict()
    {
        // Act
        var response = await Api.Customer.Orders
            .CreateAsync(cart => cart.Apples(TestProducts.Apples.Stock + 1));

        var text = await response.Content.ReadAsStringAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.Conflict)
            .And.ContainContentAsync(new AppErrorTestModel("product_stock_not_available"));
    }

    [Fact]
    public async Task When_CreatingOrder_Should_DecreaseProductsStocks()
    {
        // Arrange & Act
        await Api.Customer.Orders.CreateAsync(cart => cart.Apples(4).Bananas(5));

        // Assert
        await ProductShouldHaveStock(TestProducts.Apples.Id, 1);
        await ProductShouldHaveStock(TestProducts.Bananas.Id, 5);
    }


    private async Task<NewOrderTestModel> CreateOrder(Action<UpdateShoppingCartTestModel> orderActions)
    {
        var newOrder = await Api.Customer.Orders
            .CreateAsync(orderActions)
            .EnsureIsSuccess()
            .ContentAsAsync<IdModel>();

        var savedOrder = await Database.FindCustomerOrder(CurrentCustomer.Id, newOrder.Id);

        return new NewOrderTestModel
        {
            Id = newOrder.Id,
            OrderedAt = new DateTimeTestModel(savedOrder!.CreatedAt)
        };
    }

    private async Task ProductShouldHaveStock(string id, int expectedStock)
    {
        var product = await Api.Customer.Products
            .FindAsync(id)
            .EnsureIsSuccess()
            .ContentAsAsync<ReadProductTestModel>();

        product.Stock.Should().Be(expectedStock);
    }
}