namespace Store.Orders.Business;

public sealed record FindCustomerOrderQuery(string OrderId): IRequest<FindCustomerOrderQueryResult>;