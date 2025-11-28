using Store.Shared;

namespace Store.Products.Contracts;

public sealed record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required Price Price { get; init; }

    public required int Stock { get; init; }
    internal bool IsStockAvailable(int quantity) => quantity.IsInRange(0, Stock);
}