using Store.Orders.Domain;

namespace Store.Orders.Business;

internal sealed class GetCustomerOrdersQueryHandler(IOrdersRepository orders, ICurrentCustomer currentCustomer)
    : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderSummaryModel>>
{
    public async Task<IEnumerable<OrderSummaryModel>> Handle(GetCustomerOrdersQuery request, CancellationToken _)
    {
        var customerOrders = await orders.GetCustomerOrdersAsync(currentCustomer.Id);

        return customerOrders
            .OrderByDescending(order => order.CreatedAt)
            .Select(ToOrderSummaryModel);
    }

    private static OrderSummaryModel ToOrderSummaryModel(Order order) => new()
    {
        Id = order.Id,
        OrderedAt = DateTimeModel.Create(order.CreatedAt),

        TotalProducts = order.TotalProducts,
        TotalPrice = order.TotalPrice
    };
}