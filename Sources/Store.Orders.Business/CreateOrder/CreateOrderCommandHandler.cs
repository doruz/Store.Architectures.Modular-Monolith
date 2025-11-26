using EnsureThat;
using Store.Orders.Domain;
using Store.Products.Contracts;

namespace Store.Orders.Business;

internal sealed class CreateOrderCommandHandler(IOrdersRepository orders, ICurrentCustomer currentCustomer, IMediator mediator)
    : IRequestHandler<CreateOrderCommand, IdModel>
{
    public async Task<IdModel> Handle(CreateOrderCommand request, CancellationToken _)
    {
        request.EnsureIsNotEmpty();

        var customerOrder = new Order
        (
            currentCustomer.Id,
            await GetOrderLines(request.ValidLines)
        );

        await SaveOrder(customerOrder);

        return new IdModel(customerOrder.Id);
    }

    private async Task<IEnumerable<OrderLine>> GetOrderLines(IEnumerable<CreateOrderLineModel> lines)
    {
        var orderDetails = await lines
            .Select(async orderLine => new
            {
                OrderLine = orderLine,
                Product = await FindValidProduct(orderLine.ProductId, orderLine.Quantity)
            })
            .ToListAsync();

        return orderDetails.Select(x => CreateOrderLine(x.OrderLine, x.Product));
    }

    public async Task<FindProductResponse> FindValidProduct(string id, int expectedQuantity)
    {
        var product = await mediator.Send(new FindProductRequest(id));

        return product
            .EnsureExists(id)
            .EnsureStockIsAvailable(expectedQuantity);
    }

    private static OrderLine CreateOrderLine(CreateOrderLineModel orderLine, FindProductResponse product)
    {
        EnsureArg.IsTrue(orderLine.ProductId.IsEqualTo(product.Id));
        EnsureArg.IsInRange(orderLine.Quantity, 0, product.Stock);

        return new OrderLine
        (
            product.Id,
            product.Name,
            product.Price.Value,

            orderLine.Quantity
        );
    }

    private async Task SaveOrder(Order order)
    {
        await orders.SaveOrderAsync(order);
        await mediator.Publish(order.NewOrderEvent());
    }
}