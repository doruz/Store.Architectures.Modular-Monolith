using Store.Orders.Domain;

namespace Store.Orders.Business;

internal sealed class FindCustomerOrderQueryHandler(IOrdersRepository orders, ICurrentCustomer currentCustomer)
    : IRequestHandler<FindCustomerOrderQuery, FindCustomerOrderQueryResult>
{
    public async Task<FindCustomerOrderQueryResult> Handle(FindCustomerOrderQuery request, CancellationToken _)
    {
        var order = await orders
            .FindOrderAsync(currentCustomer.Id, request.OrderId)
            .EnsureIsNotNull(request.OrderId);

        return order.Map(ToOrderDetailedModel);
    }

    private static FindCustomerOrderQueryResult ToOrderDetailedModel(Order order) => new()
    {
        Id = order.Id,
        OrderedAt = DateTimeModel.Create(order.CreatedAt),

        TotalProducts = order.TotalProducts,
        TotalPrice = PriceModel.Create(order.TotalPrice),

        Lines = order.Lines.Select(ToOrderLineModel).ToList()
    };

    private static OrderDetailedLineModel ToOrderLineModel(OrderLine product) => new()
    {
        ProductId = product.ProductId,
        ProductName = product.ProductName,
        ProductPrice = PriceModel.Create(product.ProductPrice),
        Quantity = product.Quantity,
        TotalPrice = PriceModel.Create(product.TotalPrice)
    };
}