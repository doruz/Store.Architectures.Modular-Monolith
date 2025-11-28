using MediatR;

namespace Store.Orders.Contracts;

public sealed record NewOrderEvent(string CustomerId, string OrderId) : INotification
{
    public IEnumerable<Product> Products { get; init; } = [];

    public sealed record Product(string Id, int Quantity);
}