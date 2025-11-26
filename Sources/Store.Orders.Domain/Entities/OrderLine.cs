using EnsureThat;
using Store.Shared;

namespace Store.Orders.Domain;

public sealed record OrderLine
{
    public string ProductId { get; }

    public string ProductName { get; }

    public Price ProductPrice { get; }

    public int Quantity { get; }

    public Price TotalPrice => ProductPrice * Quantity;

    public OrderLine(string productId, string productName, Price productPrice, int quantity)
    {
        ProductId = EnsureArg.IsNotNullOrEmpty(productId);
        ProductName = EnsureArg.IsNotNullOrEmpty(productName);
        ProductPrice = EnsureArg.IsNotNull(productPrice);
        Quantity = EnsureArg.IsGte(quantity, 0);
    }
}