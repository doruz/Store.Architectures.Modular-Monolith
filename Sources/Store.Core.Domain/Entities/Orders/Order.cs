using EnsureThat;
using Store.Shared;

namespace Store.Core.Domain.Entities;

public sealed class Order(string customerId, params IEnumerable<OrderLine> lines) : Entity
{
    public string CustomerId { get; } = EnsureArg.IsNotNullOrEmpty(customerId);

    public IReadOnlyList<OrderLine> Lines { get; } = Ensure.Enumerable.HasItems(lines).ToList();

    public int TotalProducts => Lines.Sum(line => line.Quantity);

    public Price TotalPrice => Lines.Select(line => line.TotalPrice).Sum();
}