using EnsureThat;
using Store.Shared;

namespace Store.Orders.Domain;

public sealed class InMemoryOrdersRepository : IOrdersRepository
{
    private readonly List<Order> _orders = [];

    public Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));

        var customerOrders = _orders
            .Where(order => order.CustomerId.IsEqualTo(customerId));

        return Task.FromResult(customerOrders);
    }

    public Task<Order?> FindOrderAsync(string customerId, string id)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        var customerOrder = _orders
            .FirstOrDefault(order => order.CustomerId.IsEqualTo(customerId) && order.Id.IsEqualTo(id));

        return Task.FromResult(customerOrder);
    }

    public Task SaveOrderAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        _orders.Add(order);

        return Task.CompletedTask;
    }
}