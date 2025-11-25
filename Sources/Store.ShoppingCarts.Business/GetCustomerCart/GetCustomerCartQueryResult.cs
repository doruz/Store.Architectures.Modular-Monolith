namespace Store.ShoppingCarts.Business;

public sealed record GetCustomerCartQueryResult(IEnumerable<GetCustomerCartLineModel> Lines)
{
    public PriceModel TotalPrice => Lines.Sum(l => l.TotalPrice);
}

public sealed record GetCustomerCartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceModel ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public PriceModel TotalPrice => ProductPrice * Quantity;
}
