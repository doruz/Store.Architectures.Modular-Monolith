using EnsureThat;
using Store.Orders.Domain;
using Store.Products.Contracts;

namespace Store.Orders.Business;

internal sealed class CreateOrderCommandHandler(IOrdersRepository orders, ICurrentCustomer currentCustomer, ISender mediator)
    : IRequestHandler<CreateOrderCommand, IdModel>
{
    public async Task<IdModel> Handle(CreateOrderCommand request, CancellationToken _)
    {
        var customerOrder = new Order
            (
                currentCustomer.Id,
                await GetOrderLines(request.ValidLines)
            )
            .EnsureIsNotEmpty();



        await orders.SaveOrderAsync(customerOrder);

        // TODO: update stock & clear cart

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
            .EnsureIsNotNull(id)
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

    //    // TODO: when order is saved it should update products stocks
    //    //private async Task UpdateProductsStock(List<(ShoppingCartLine CartLine, FindProductResponse Product)> items)
    //    //{
    //    //    foreach (var item in items)
    //    //    {
    //    //        item.Product.DecreaseStock(item.CartLine.Quantity);
    //    //        await repositories.Products.UpdateAsync(item.Product);
    //    //    }
    //    //}
}