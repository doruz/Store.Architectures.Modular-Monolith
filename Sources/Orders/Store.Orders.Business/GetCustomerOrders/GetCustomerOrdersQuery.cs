namespace Store.Orders.Business;

public sealed record GetCustomerOrdersQuery : IRequest<IEnumerable<OrderSummaryModel>>;
